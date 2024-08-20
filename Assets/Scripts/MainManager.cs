using System.Linq;
using Cysharp.Threading.Tasks;
using SimplePainterServer.Dto;
using UnityEngine;

namespace Painter
{
    public class MainManager : MonoSingleton<MainManager>
    {
        [field: SerializeField] public Transform UIRoot       { get; private set; }
        AssetManager                             AssetManager => AssetManager.Instance;
        NetManager                               NetManager   => NetManager.Instance;
        int                                      _userId;
        TaskSelectPanel                          _taskSelectPanel;

        void Start()
        {
            In().Forget();
            return;

            async UniTask In()
            {
                var loadPrefab = await UILoader.LoadPrefab(AssetManager.LoginPanelPrefab);

                loadPrefab.OnLogin += OnLoadPrefabOnOnLogin;
                return;

                void OnLoadPrefabOnOnLogin(int id)
                {
                    _userId = id;
                    LoadTaskSelectPanel().Forget();
                }
            }
        }

        async UniTaskVoid LoadTaskSelectPanel()
        {
            _taskSelectPanel                  =  await UILoader.LoadPrefab(AssetManager.TaskSelectPanelPrefab);
            _taskSelectPanel.OnDraw           += () => LoadDrawPanel().Forget();
            _taskSelectPanel.OnGuess          += () => LoadGuessPanel().Forget();
            _taskSelectPanel.OnUserStatistics += () => LoadUserStatisticsPanel().Forget();
            _taskSelectPanel.OnWordStatistics += () => LoadWordStatisticsPanel().Forget();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid LoadWordStatisticsPanel()
        {
            var panel = await UILoader.LoadPrefab(AssetManager.WordStatisticsPanelPrefab);
            panel.Close += () => Destroy(panel.gameObject);
            var data = await NetManager.GetWordInfoDetailList();
            panel.SetData(data).Forget();
        }

        async UniTaskVoid LoadUserStatisticsPanel()
        {
            var userStatisticsPanel = await UILoader.LoadPrefab(AssetManager.UserStatisticPanelPrefab);
            userStatisticsPanel.Close += () => Destroy(userStatisticsPanel.gameObject);
            var userStatistics = await NetManager.GetUserInfoDetailList();
            userStatisticsPanel.SetData(userStatistics).Forget();
            userStatisticsPanel.OnDrawDetailClick  += value => LoadUserDrawStatisticsPanel(value).Forget();
            userStatisticsPanel.OnGuessDetailClick += value => LoadUserGuessStatisticsPanel(value).Forget();
        }

        async UniTaskVoid LoadUserDrawStatisticsPanel(int userId)
        {
            var window = await UILoader.LoadPrefab(AssetManager.UserDrawDetailWindowPrefab);
            window.Close += () => Destroy(window.gameObject);
            var data = await NetManager.GetImageInfoListForUser(userId);
            window.SetData(data).Forget();
        }

        async UniTaskVoid LoadUserGuessStatisticsPanel(int userId)
        {
            var window = await UILoader.LoadPrefab(AssetManager.UserGuessDetailWindowPrefab);
            window.Close += () => Destroy(window.gameObject);
            var data = await NetManager.GetGuessListForUser(userId);
            window.SetData(data).Forget();
        }

        Texture2D _currentTexture;

        async UniTaskVoid LoadDrawPanel()
        {
            var taskSelectPanel = await UILoader.LoadPrefab(AssetManager.DrawPanelPrefab);
            taskSelectPanel.OnReturn += () => { Destroy(taskSelectPanel.gameObject); };
            taskSelectPanel.OnClear  += () => { taskSelectPanel.Clear(); };
            var infos = await NetManager.GetAllWords();
            foreach (var wordInfoDto in infos.TakeWhile(_ => taskSelectPanel))
            {
                taskSelectPanel.SetWord(wordInfoDto.Name);
                await taskSelectPanel.WaitSubmit();
                NetManager.Instance.PostImage(new ImageInfoDto(0, wordInfoDto.ID, _userId), taskSelectPanel.DrawTexture)
                    .Forget();
                taskSelectPanel.Clear();
            }

            if (taskSelectPanel)
                taskSelectPanel.EndAllWord();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid LoadGuessPanel()
        {
            var taskSelectPanel = await UILoader.LoadPrefab(AssetManager.GuessPanelPrefab);
            taskSelectPanel.OnReturn += () => { Destroy(taskSelectPanel.gameObject); };
            var infos = await NetManager.GetImageList();
            foreach (var imageInfoDto in infos.TakeWhile(_ => taskSelectPanel))
            {
                var imageTexture = await NetManager.Instance.GetImageTexture(imageInfoDto.ID);
                taskSelectPanel.SetTexture(imageTexture);
                var word = await taskSelectPanel.WaitNext();
                NetManager.PostGuess(new(0, imageInfoDto.ID, word, _userId)).Forget();
            }

            if (taskSelectPanel)
                taskSelectPanel.EndAll();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid UpdateDrawGuessCompletion()
        {
            _taskSelectPanel.UpdateDrawGuessCompletion(await NetManager.DrawGuessCompletion());
        }
    }
}