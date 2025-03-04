using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Painter
{
    public class WordStatisticsItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI nameText;

        [SerializeField]
        TextMeshProUGUI partSpeechText;

        [SerializeField]
        TextMeshProUGUI drawCountText;

        [SerializeField]
        Button _clearButton;

        [SerializeField]
        Button _removeButton;

        [SerializeField]
        Button _timeButton;

        public event UnityAction OnTime
        {
            add => _timeButton.onClick.AddListener(value);
            remove => _timeButton.onClick.RemoveListener(value);
        }

        public event Action OnDrawDetail;
        public event Action OnClear;
        public event Action OnRemove;
        public void         ClickDrawDetail() => OnDrawDetail?.Invoke();
        public void         ClickClear()      => OnClear?.Invoke();
        public void         ClickRemove()     => OnRemove?.Invoke();

        public void SetData(string inName, string partSpeech, int drawCount, bool enableClear)
        {
            nameText.text       = inName;
            partSpeechText.text = partSpeech;
            drawCountText.text  = drawCount.ToString();
            _clearButton.gameObject.SetActive(enableClear);
            _removeButton.gameObject.SetActive(enableClear);
        }
    }
}