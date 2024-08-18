using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class GuessPanel : MonoBehaviour
    {
        [SerializeField] TMP_InputField _word;
        [SerializeField] RawImage       _drawArea;
        [SerializeField] GameObject     _overTip;
        public           void           SetTexture(Texture2D texture2D) => _drawArea.texture = texture2D;
        public event Action             OnNext;
        public event Action             OnReturn;

        public void NextClick()
        {
            OnNext?.Invoke();
        }

        public void ReturnClick()
        {
            OnReturn?.Invoke();
        }
    }
}