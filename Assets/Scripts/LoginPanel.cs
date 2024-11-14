using System;
using Cysharp.Threading.Tasks;
using SimplePainterServer.Dto;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField _userNameInputField;

        [SerializeField]
        TMP_InputField _languageInputField;

        [SerializeField]
        TMP_InputField _sexInputField;

        [SerializeField]
        TMP_InputField _ageInputField;

        [SerializeField]
        TMP_InputField _careerInputField;

        [SerializeField]
        TMP_InputField _educationLevelInputField;

        [SerializeField]
        GameObject _errorTip;

        public event Action<UserInfoDto> OnLogin;

        public void LoginClick()
        {
            UniTask.Action(async () =>
            {
                if (!string.IsNullOrWhiteSpace(_userNameInputField.text) && UserNameOtherIsBlank())
                {
                    var userInfoDto = await NetManager.Instance.Login(_userNameInputField.text);
                    if (userInfoDto == null)
                        _errorTip.SetActive(true);
                    else
                    {
                        OnLogin?.Invoke(userInfoDto);
                        Destroy(gameObject);
                    }
                }
                else if (UserNameOtherIsBlank())
                    _errorTip.SetActive(true);
                else
                    Login().Forget();
            }).Invoke();
            return;

            bool UserNameOtherIsBlank() =>
                string.IsNullOrWhiteSpace(_languageInputField.text) || string.IsNullOrWhiteSpace(_sexInputField.text) ||
                string.IsNullOrWhiteSpace(_ageInputField.text) || string.IsNullOrWhiteSpace(_careerInputField.text) ||
                string.IsNullOrWhiteSpace(_educationLevelInputField.text);

            async UniTaskVoid Login()
            {
                UserInfoDto userInfoDto = new(0, _userNameInputField.text, _languageInputField.text,
                                              _sexInputField.text, _ageInputField.text, _careerInputField.text,
                                              _educationLevelInputField.text);
                var infoDto = await NetManager.Instance.Login(userInfoDto);
                OnLogin?.Invoke(infoDto);
                Destroy(gameObject);
            }
        }
    }
}