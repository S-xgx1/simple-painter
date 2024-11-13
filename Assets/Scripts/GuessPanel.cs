using System;
using System.Threading.Tasks;
using PunctualSolutions.Tool.UniTask;
using PunctualSolutionsTool.Tool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Painter
{
    public class GuessPanel : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField _word;

        [SerializeField]
        RawImage _drawArea;

        [SerializeField]
        GameObject _overTip;

        [SerializeField]
        GameObject _wordRoot;

        [SerializeField]
        GameObject _nextButton;

        [SerializeField]
        GameObject _beginTip;

        [SerializeField]
        GameObject _nextTip;

        [SerializeField]
        GameObject _submitTip;

        public void                 SetTexture(Texture2D texture2D) => _drawArea.texture = texture2D;
        public event Action         OnNext;
        public event Action         OnReturn;
        public event Action<string> OnSubmit;

        public Task WaitNext() =>
            TaskConvertTool.WaitTask(x => OnNext += x, x => OnNext -= x,
                                     destroyCancellationToken.CreateLinkedTokenSource());

        public Task<string> WaitSubmit() =>
            TaskConvertTool.WaitTask<string>(x => OnSubmit += x, x => OnSubmit -= x,
                                             destroyCancellationToken.CreateLinkedTokenSource());

        public void NextClick()
        {
            OnNext?.Invoke();
            _beginTip.SetActive(false);
            _submitTip.SetActive(false);
            _nextTip.SetActive(true);
        }

        public void SubmitClick()
        {
            OnSubmit?.Invoke(_word.text);
            _beginTip.SetActive(false);
            _nextTip.SetActive(false);
            _submitTip.SetActive(true);
        }

        public void ReturnClick()
        {
            OnReturn?.Invoke();
        }

        public void EndAll()
        {
            _overTip.SetActive(true);
            _drawArea.gameObject.SetActive(false);
            _wordRoot.SetActive(false);
            _nextButton.SetActive(false);
        }
    }
}