using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class TaskSelectPanel : MonoBehaviour
    {
        [SerializeField]
        Image DrawGuessCompletionProgress;

        [SerializeField]
        TextMeshProUGUI DrawGuessCompletionText;

        public event Action OnDraw;
        public event Action OnGuess;
        public event Action OnWordStatistics;
        public event Action OnUserStatistics;

        public void DrawClick()
        {
            OnDraw?.Invoke();
        }

        public void GuessClick()
        {
            OnGuess?.Invoke();
        }

        public void UserStatisticsClick()
        {
            OnUserStatistics?.Invoke();
        }

        public void WordStatisticsClick()
        {
            OnWordStatistics?.Invoke();
        }

        public void UpdateDrawGuessCompletion(float completion)
        {
            DrawGuessCompletionProgress.fillAmount = completion;
            DrawGuessCompletionText.text           = (int)(completion * 100) + "%";
        }
    }
}