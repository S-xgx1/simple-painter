using System;
using UnityEngine;

namespace Painter
{
    public class TaskSelectPanel : MonoBehaviour
    {
        public event Action OnDraw;
        public event Action OnGuess;
        public event Action OnStatistics;

        public void DrawClick()
        {
            OnDraw?.Invoke();
        }

        public void GuessClick()
        {
            OnGuess?.Invoke();
        }

        public void StatisticsClick()
        {
            OnStatistics?.Invoke();
        }
    }
}