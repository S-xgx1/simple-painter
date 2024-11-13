using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Painter.Dto;
using UnityEngine;

namespace Painter
{
    public class UserDrawDetailWindow : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField]
        RectTransform _content;

        public async UniTaskVoid SetData(List<ImageInfoDetail> data)
        {
            foreach (var imageInfoDetail in data)
            {
                var item         = await UILoader.LoadPrefab(AssetManager.Instance.UserDrawDetailItemPrefab, _content);
                var imageTexture = await NetManager.Instance.GetImageTexture(imageInfoDetail.ID);
                item.SetData(imageTexture, imageInfoDetail.GuessWord);
            }
        }
    }
}