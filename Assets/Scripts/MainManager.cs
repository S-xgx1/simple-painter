using System.Linq;
using Cysharp.Threading.Tasks;
using PunctualSolutions.Tool.Singleton;
using SimplePainterServer.Dto;
using UnityEngine;

namespace Painter
{
    [SingletonMono]
    public partial class MainManager : MonoBehaviour
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

        static AssetManager AssetManager => AssetManager.Instance;
        static NetManager   NetManager   => NetManager.Instance;
        UserInfoDto         _userInfo;
        TaskSelectPanel     _taskSelectPanel;

        void Start()
        {
            In().Forget();
            return;

            async UniTask In()
            {
                var loadPrefab = await UILoader.LoadPrefab(AssetManager.LoginPanelPrefab);

                loadPrefab.OnLogin += LoadPrefabOnOnLogin;
                return;

                void LoadPrefabOnOnLogin(UserInfoDto userInfo)
                {
                    _userInfo = userInfo;
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
            panel.SetData(data, _userInfo.Name == NetManager.ADMIN_NAME).Forget();
            panel.Add += () => { LoadAddWordPanel(panel).Forget(); };
        }

        async UniTaskVoid LoadAddWordPanel(WordStatisticsPanel wordStatisticsPanel)
        {
            var panel = await UILoader.LoadPrefab(AssetManager.AddWordPanelPrefab);
            panel.Close += UniTask.Action(async () =>
            {
                Destroy(panel.gameObject);
                var data = await NetManager.GetWordInfoDetailList();
                wordStatisticsPanel.SetData(data, _userInfo.Name == NetManager.ADMIN_NAME).Forget();
            });
            panel.Add += PanelOnAdd;
            return;

            void PanelOnAdd(string word, string partOfSpeech)
            {
                NetManager.AddWord(word, partOfSpeech).Forget();
            }
        }

        async UniTaskVoid LoadUserStatisticsPanel()
        {
            var panel = await UILoader.LoadPrefab(AssetManager.UserStatisticPanelPrefab);
            panel.Close += () => Destroy(panel.gameObject);
            var userStatistics = await NetManager.GetUserInfoDetailList();
            panel.SetData(userStatistics, NetManager.ADMIN_NAME == _userInfo.Name).Forget();
            panel.OnDrawDetailClick  += value => LoadUserDrawStatisticsPanel(value).Forget();
            panel.OnGuessDetailClick += value => LoadUserGuessStatisticsPanel(value).Forget();
            panel.OnDeleteClick += value =>
            {
                NetManager.DeleteUser(value).Forget();
                panel.DestroyItem(value);
            };
        }

        static async UniTaskVoid LoadUserDrawStatisticsPanel(int userId)
        {
            var window = await UILoader.LoadPrefab(AssetManager.UserDrawDetailWindowPrefab);
            window.Close += () => Destroy(window.gameObject);
            var data = await NetManager.GetImageInfoListForUser(userId);
            window.SetData(data).Forget();
        }

        static async UniTaskVoid LoadUserGuessStatisticsPanel(int userId)
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
                await Wait();
                continue;

                async UniTask Wait()
                {
                    taskSelectPanel.SetWord(wordInfoDto.Name);
                    var whenAny = await UniTask.WhenAny(taskSelectPanel.WaitSubmit().AsUniTask(),
                                                        taskSelectPanel.WaitNext().AsUniTask());
                    switch (whenAny)
                    {
                        case 0 when taskSelectPanel.Change:
                            NetManager.Instance
                                      .PostImage(new(0, wordInfoDto.ID, _userInfo.ID), taskSelectPanel.DrawTexture)
                                      .Forget();
                            break;
                        case 0 when !taskSelectPanel.Change:
                            await Wait();
                            break;
                    }

                    taskSelectPanel.Clear();
                }
            }

            if (taskSelectPanel)
                taskSelectPanel.EndAllWord();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid LoadGuessPanel()
        {
            var taskSelectPanel = await UILoader.LoadPrefab(AssetManager.GuessPanelPrefab);
            taskSelectPanel.OnReturn += () => Destroy(taskSelectPanel.gameObject);
            var infos = await NetManager.GetImageList();
            foreach (var imageInfoDto in infos.TakeWhile(_ => taskSelectPanel))
            {
                await Wait();
                continue;

                async UniTask Wait()
                {
                    while (true)
                    {
                        var imageTexture         = await NetManager.Instance.GetImageTexture(imageInfoDto.ID);
                        var imageTextureTipWords = await NetManager.Instance.GetImageTextureTipWords(imageInfoDto.ID);
                        taskSelectPanel.SetTexture(imageTexture, imageTextureTipWords);
                        var whenAny = await UniTask.WhenAny(taskSelectPanel.WaitSubmit().AsUniTask(),
                                                            taskSelectPanel.WaitNext().AsUniTask());
                        switch (whenAny.hasResultLeft)
                        {
                            case true when !string.IsNullOrWhiteSpace(whenAny.result):
                                NetManager.PostGuess(new(0, imageInfoDto.ID, whenAny.result, _userInfo.ID)).Forget();
                                break;
                            case true when string.IsNullOrWhiteSpace(whenAny.result):
                                continue;
                        }

                        break;
                    }
                }
            }

            if (taskSelectPanel)
                taskSelectPanel.EndAll();
            UpdateDrawGuessCompletion().Forget();
        }

        async UniTaskVoid UpdateDrawGuessCompletion() =>
            _taskSelectPanel.UpdateDrawGuessCompletion(await NetManager.DrawGuessCompletion());
    }
}