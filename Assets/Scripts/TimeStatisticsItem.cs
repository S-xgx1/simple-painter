using System;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class TimeStatisticsItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _dateTimeText;

        public void SetData(DateTime dateTime)
        {
            _dateTimeText.text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}