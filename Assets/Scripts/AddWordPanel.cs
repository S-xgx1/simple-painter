using System;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class AddWordPanel : MonoBehaviour
    {
        public delegate void AddWordDelegate(string word, string partOfSpeech);

        public event Action          Close;
        public event AddWordDelegate Add;

        public void CloseClick()
        {
            Close?.Invoke();
        }

        public void AddClick()
        {
            Add?.Invoke(_wordText.text, _partOfSpeechText.text);
            CloseClick();
        }

        [SerializeField]
        TMP_InputField _wordText;

        [SerializeField]
        TMP_InputField _partOfSpeechText;
    }
}