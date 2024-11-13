using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class DrawDetailItem : MonoBehaviour
    {
        [SerializeField]
        RawImage _image;

        [SerializeField]
        TextMeshProUGUI _guessCountText;

        [SerializeField]
        TextMeshProUGUI _guessCorrectlyProportionText;

        public event Action OnGuessDetail;
        public void         ClickGuessDetail() => OnGuessDetail?.Invoke();

        public void SetData(Texture2D texture, int guessCount, float guessCorrectlyProportion)
        {
            _image.texture                     = texture;
            _guessCountText.text               = guessCount.ToString();
            _guessCorrectlyProportionText.text = guessCorrectlyProportion.ToString("0.00%");
        }
    }
}