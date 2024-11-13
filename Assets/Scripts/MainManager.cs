using System.Linq;
using Cysharp.Threading.Tasks;
using PunctualSolutions.Tool.Singleton;
using SimplePainterServer.Dto;
using UnityEngine;

namespace Painter
{
    [SingletonMono]
    public class MainManager : MonoBehaviour
    {
        public void OnSingletonInit()
        {
        }

        public void Dispose()
        {
        }

        static void InAwake()
        {
        }

        [field: SerializeField]
        public Transform UIRoot { get; private set; }

        AssetManager    AssetManager => AssetManager.Instance;
        NetManager      NetManager   => NetManager.Instance;
        int             _userId;
        TaskSelectPanel _taskSelectPanel;

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
            var panel = await UILoader.LoadPrefab(AssetManager.UserStatisticPanelPrefab);
            panel.Close += () => Destroy(panel.gameObject);
            var userStatistics = await NetManager.GetUserInfoDetailList();
            panel.SetData(userStatistics).Forget();
            panel.OnDrawDetailClick  += value => LoadUserDrawStatisticsPanel(value).Forget();
            panel.OnGuessDetailClick += value => LoadUserGuessStatisticsPanel(value).Forget();
            panel.OnDeleteClick += value =>
            {
                NetManager.DeleteUser(value).Forget();
                panel.DestroyItem(value);
            };
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
            taskSelectPanel.OnReturn += () => Destroy(taskSelectPanel.gameObject);
            taskSelectPanel.OnClear  += () => taskSelectPanel.Clear();
            var infos = await NetManager.GetAllWords();
            foreach (var wordInfoDto in infos.TakeWhile(_ => taskSelectPanel))
            {
                taskSelectPanel.SetWord(wordInfoDto.Name);
                var whenAny = await UniTask.WhenAny(taskSelectPanel.WaitSubmit().AsUniTask(),
                                                    taskSelectPanel.WaitNext().AsUniTask());
                if (whenAny == 0)
                    NetManager.Instance.PostImage(new(0, wordInfoDto.ID, _userId), taskSelectPanel.DrawTexture)
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
                var whenAny = await UniTask.WhenAny(taskSelectPanel.WaitSubmit().AsUniTask(),
                                                    taskSelectPanel.WaitNext().AsUniTask());
                if (whenAny.hasResultLeft)
                    NetManager.PostGuess(new(0, imageInfoDto.ID, whenAny.result, _userId)).Forget();
            }

            if (taskSelectPanel)
                taskSelectPanel.EndAll();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid UpdateDrawGuessCompletion() =>
            _taskSelectPanel.UpdateDrawGuessCompletion(await NetManager.DrawGuessCompletion());
    }
}