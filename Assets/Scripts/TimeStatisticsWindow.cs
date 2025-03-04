using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Painter.SimplePainterServer.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class TimeStatisticsWindow : MonoBehaviour
    {
        [SerializeField]
        Button _closeButton;

        [SerializeField]
        RectTransform _contentImage;

        [SerializeField]
        RectTransform _contentGuess;

        void Awake()
        {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        }

        public async UniTaskVoid SetData(List<WordCreateTimeDto> dateTimes)
        {
            foreach (var dateTime in dateTimes.Where(x => x.Type == CreateTimeType.Image))
            {
                var item = await UILoader.LoadPrefab(AssetManager.Instance.TimeStatisticsItem, _contentImage);
                item.SetData(dateTime.DateTime);
            }

            foreach (var dateTime in dateTimes.Where(x => x.Type == CreateTimeType.Guess))
            {
                var item = await UILoader.LoadPrefab(AssetManager.Instance.TimeStatisticsItem, _contentGuess);
                item.SetData(dateTime.DateTime);
            }
        }
    }
}