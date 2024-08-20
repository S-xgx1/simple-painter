using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class UserGuessDetailItem : MonoBehaviour
    {
        [SerializeField] RawImage        GuessImage;
        [SerializeField] TextMeshProUGUI GuessText;
        [SerializeField] TextMeshProUGUI WordText;

        public void SetData(Texture2D guess, string guessText, string wordText)
        {
            GuessImage.texture = guess;
            GuessText.text     = guessText;
            WordText.text      = wordText;
        }
    }
}