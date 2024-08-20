using System;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class WordStatisticsItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI partSpeechText;
        [SerializeField] TextMeshProUGUI drawCountText;
        public event Action              OnDrawDetail;
        public void                      ClickDrawDetail() => OnDrawDetail?.Invoke();

        public void SetData(string inName, string partSpeech, int drawCount)
        {
            nameText.text       = inName;
            partSpeechText.text = partSpeech;
            drawCountText.text  = drawCount.ToString();
        }
    }
}