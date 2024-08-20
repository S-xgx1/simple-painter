using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class WordStatisticsPanel : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField] RectTransform _content;

        public async UniTaskVoid SetData(List<WordInfoDetail> data)
        {
            foreach (var info in data)
            {
                var item = await UILoader.LoadPrefab(AssetManager.Instance.WordStatisticsItemPrefab, _content);
                item.SetData(info.Name, info.PartSpeech, info.DrawCount);
                item.OnDrawDetail += () => LoadDrawDetail().Forget();
                continue;

                async UniTaskVoid LoadDrawDetail()
                {
                    var window = await UILoader.LoadPrefab(AssetManager.Instance.DrawDetailWindowPrefab);
                    window.SetData(info.ID).Forget();
                    window.Close += () => Destroy(window.gameObject);
                }
            }
        }
    }
}