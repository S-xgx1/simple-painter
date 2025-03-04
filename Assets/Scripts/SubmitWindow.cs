using System;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class SubmitWindow : MonoBehaviour
    {
        public event Action Close;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        [SerializeField]
        TextMeshProUGUI _contentText;

        public string Content
        {
            set => _contentText.text = value;
        }
    }
}