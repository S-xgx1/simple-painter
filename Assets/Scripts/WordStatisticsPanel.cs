using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Painter
{
    public class WordStatisticsPanel : MonoBehaviour
    {
        public event Action Close;
        public event Action Add;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField]
        RectTransform _content;

        readonly List<WordStatisticsItem> _items = new();

        public void AddClick()
        {
            Add?.Invoke();
        }

        public async UniTaskVoid SetData(List<WordInfoDetail> data, bool isAdmin)
        {
            foreach (var wordStatisticsItem in _items) Destroy(wordStatisticsItem.gameObject);
            _items.Clear();
            foreach (var info in data)
            {
                var item = await UILoader.LoadPrefab(AssetManager.Instance.WordStatisticsItemPrefab, _content);
                item.SetData(info.Name, info.PartSpeech, info.DrawCount, isAdmin);
                item.OnDrawDetail += () => LoadDrawDetail().Forget();
                item.OnClear += () =>
                {
                    NetManager.Instance.ClearWord(info.ID).Forget();
                    item.SetData(info.Name, info.PartSpeech, 0, isAdmin);
                };
                item.OnRemove += () =>
                {
                    NetManager.Instance.DeleteWord(info.ID).Forget();
                    Destroy(item.gameObject);
                };
                item.OnTime += UniTask.UnityAction(async () =>
                {
                    var window = await UILoader.LoadPrefab(AssetManager.Instance.TimeStatisticsWindow);
                    window.SetData(await NetManager.Instance.GetCreateTimeList(info.ID)).Forget();
                });
                _items.Add(item);
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