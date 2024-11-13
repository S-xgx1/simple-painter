using TMPro;
using UnityEngine;

namespace Painter
{
    public class GuessDetailItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI guessText;

        public void SetData(string infoWord)
        {
            guessText.text = infoWord;
        }
    }
}