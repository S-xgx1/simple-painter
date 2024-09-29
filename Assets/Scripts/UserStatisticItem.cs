using System;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class UserStatisticItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Name;
        [SerializeField] TextMeshProUGUI Language;
        [SerializeField] TextMeshProUGUI Sex;
        [SerializeField] TextMeshProUGUI Age;
        [SerializeField] TextMeshProUGUI Career;
        [SerializeField] TextMeshProUGUI EducationLevel;
        [SerializeField] TextMeshProUGUI DrawImageCount;
        [SerializeField] TextMeshProUGUI GuessImageCount;
        [SerializeField] TextMeshProUGUI GuessImageProbability;
        public event Action              OnDrawDetailClick;
        public event Action              OnGuessDetailClick;
        public event Action              OnDeleteClick;

        public void DrawDetailClick()
        {
            OnDrawDetailClick?.Invoke();
        }

        public void GuessDetailClick()
        {
            OnGuessDetailClick?.Invoke();
        }

        public void DeleteClick() => OnDeleteClick?.Invoke();

        public void SetData(string inName, string language, string sex, string age, string career,
            string                 educationLevel,
            int                    drawImageCount, int guessImageCount, float guessImageProbability)
        {
            Name.text                  = inName;
            Language.text              = language;
            Sex.text                   = sex;
            Age.text                   = age;
            Career.text                = career;
            EducationLevel.text        = educationLevel;
            DrawImageCount.text        = drawImageCount.ToString();
            GuessImageCount.text       = guessImageCount.ToString();
            GuessImageProbability.text = guessImageProbability.ToString("0.00%");
        }
    }
}