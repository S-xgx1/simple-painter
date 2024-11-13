using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class UserDrawDetailItem : MonoBehaviour
    {
        [SerializeField]
        RawImage DrawImage;

        [SerializeField]
        TextMeshProUGUI WordText;

        public void SetData(Texture2D drawImage, string wordText)
        {
            DrawImage.texture = drawImage;
            WordText.text     = wordText;
        }
    }
}