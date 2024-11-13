using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SimplePainterServer.Dto;
using UnityEngine;

namespace Painter
{
    public class GuessDetailWindow : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField]
        RectTransform _content;

        public async UniTaskVoid SetData(List<GuessDto> data)
        {
            foreach (var info in data)
            {
                var item = await UILoader.LoadPrefab(AssetManager.Instance.GuessDetailItemPrefab, _content);
                item.SetData(info.Word);
            }
        }
    }
}