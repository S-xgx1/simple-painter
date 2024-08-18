using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class MainManager : MonoSingleton<MainManager>
    {
        [SerializeField] Transform UIRoot;
        AssetManager               AssetManager => AssetManager.Instance;
        int                        _userId;

        void Start()
        {
            In().Forget();
            return;

            async UniTask In()
            {
                var loadPrefab = await LoadPrefab(AssetManager.LoginPanelPrefab);

                void OnLoadPrefabOnOnLogin(int id)
                {
                    _userId = id;
                    LoadTaskSelectPanel().Forget();
                }

                loadPrefab.OnLogin += OnLoadPrefabOnOnLogin;
            }
        }

        async UniTaskVoid LoadTaskSelectPanel()
        {
            var taskSelectPanel = await LoadPrefab(AssetManager.TaskSelectPanelPrefab);
            taskSelectPanel.OnDraw       += () => LoadDrawPanel().Forget();
            taskSelectPanel.OnGuess      += () => LoadGuessPanel().Forget();
            taskSelectPanel.OnStatistics += () => LoadStatisticsPanel().Forget();
        }

        Texture2D currentTexture;

        async UniTaskVoid LoadDrawPanel()
        {
            var taskSelectPanel = await LoadPrefab(AssetManager.DrawPanelPrefab);
            taskSelectPanel.OnReturn += () =>
            {
                currentTexture = taskSelectPanel.DrawTexture;
                Destroy(taskSelectPanel.gameObject);
            };
            taskSelectPanel.OnClear += () => { taskSelectPanel.Clear(); };
        }

        async UniTaskVoid LoadGuessPanel()
        {
            var taskSelectPanel = await LoadPrefab(AssetManager.GuessPanelPrefab);
            taskSelectPanel.OnReturn += () => { Destroy(taskSelectPanel.gameObject); };
            taskSelectPanel.SetTexture(currentTexture);
        }

        async UniTaskVoid LoadStatisticsPanel() => UniTask.NextFrame();

        async UniTask<T> LoadPrefab<T>(T prefab) where T : Component
        {
            var async = InstantiateAsync(prefab, UIRoot);
            await async;
            var instance = async.Result[0];
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            return instance;
        }
    }
}