using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class DrawDetailWindow : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField]
        RectTransform _content;

        public async UniTaskVoid SetData(int wordId)
        {
            var infos = await NetManager.Instance.GetImageInfoListForWord(wordId);
            foreach (var info in infos)
            {
                var item         = await UILoader.LoadPrefab(AssetManager.Instance.DrawDetailItemPrefab, _content);
                var imageTexture = await NetManager.Instance.GetImageTexture(info.ID);
                item.SetData(imageTexture, info.GuessCount,
                             info.CorrectCount == 0 ? 0 : (float)info.CorrectCount / info.GuessCount);
                item.OnGuessDetail += () => LoadGuessDetail(info.ID).Forget();
            }
        }

        static async UniTaskVoid LoadGuessDetail(int imageID)
        {
            var window = await UILoader.LoadPrefab(AssetManager.Instance.GuessDetailWindowPrefab);
            var data   = await NetManager.Instance.GetGuessListForImage(imageID);
            window.SetData(data).Forget();
            window.Close += () => Destroy(window.gameObject);
        }
    }
}