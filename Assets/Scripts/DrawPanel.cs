using System;
using System.Threading.Tasks;
using PunctualSolutions.Tool.UniTask;
using PunctualSolutionsTool.Tool;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class DrawPanel : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _word;

        [SerializeField]
        PixelCanvasView _pixelCanvasView;

        public bool Change => _pixelCanvasView.pixelCanvas.Change;

        [SerializeField]
        GameObject _drawArea;

        [SerializeField]
        GameObject _clearButton;

        [SerializeField]
        GameObject _submitButton;

        [SerializeField]
        GameObject _returnButton;

        [SerializeField]
        GameObject _beginTip;

        [SerializeField]
        GameObject _submitTip;

        [SerializeField]
        GameObject _nextTip;

        public Texture2D DrawTexture => _pixelCanvasView.Texture;

        public void Clear()
        {
            if (_pixelCanvasView)
                _pixelCanvasView.Clear();
        }

        public event Action OnClear;
        public event Action OnSubmit;
        public event Action OnNext;
        public event Action OnReturn;

        public Task WaitSubmit() =>
            TaskConvertTool.WaitTask(x => OnSubmit += x, x => OnSubmit -= x,
                                     destroyCancellationToken.CreateLinkedTokenSource());

        public Task WaitNext() =>
            TaskConvertTool.WaitTask(x => OnNext += x, x => OnNext -= x,
                                     destroyCancellationToken.CreateLinkedTokenSource());

        public void SetWord(string word)
        {
            _word.text = word;
        }

        public void ClearClick()
        {
            OnClear?.Invoke();
        }

        public void NextClick()
        {
            OnNext?.Invoke();
            _beginTip.SetActive(false);
            _submitTip.SetActive(false);
            _nextTip.SetActive(true);
        }

        public void SubmitClick()
        {
            OnSubmit?.Invoke();
            _beginTip.SetActive(false);
            _nextTip.SetActive(false);
            _submitTip.SetActive(true);
        }

        public void ReturnClick()
        {
            OnReturn?.Invoke();
        }

        public void EndAllWord()
        {
            _word.text = "All the words have been created.";
            _drawArea.SetActive(false);
            _clearButton.SetActive(false);
            _submitButton.SetActive(false);
        }
    }
}