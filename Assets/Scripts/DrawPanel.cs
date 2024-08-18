using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class DrawPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _word;
        [SerializeField] PixelCanvasView _pixelCanvasView;
        public           Texture2D       DrawTexture => _pixelCanvasView.Texture;
        public           void            Clear()     => _pixelCanvasView.Clear();
        public event Action              OnClear;
        public event Action              OnSubmit;
        public event Action              OnReturn;

        public void ClearClick()
        {
            OnClear?.Invoke();
        }

        public void SubmitClick()
        {
            OnSubmit?.Invoke();
        }

        public void ReturnClick()
        {
            OnReturn?.Invoke();
        }
    }
}