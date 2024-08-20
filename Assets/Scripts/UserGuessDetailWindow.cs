using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class UserGuessDetailWindow : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField] RectTransform _content;

        public async UniTaskVoid SetData(List<GuessDetail> data)
        {
            foreach (var detail in data)
            {
                var item         = await UILoader.LoadPrefab(AssetManager.Instance.UserGuessDetailItemPrefab, _content);
                var imageTexture = await NetManager.Instance.GetImageTexture(detail.ImageId);
                item.SetData(imageTexture, detail.Word, detail.SuccessWord);
            }
        }
    }
}