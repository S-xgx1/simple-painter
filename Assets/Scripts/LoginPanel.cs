using System;
using Painter.Dto;
using TMPro;
using UnityEngine;

namespace Painter
{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField] TMP_InputField _userNameInputField;
        [SerializeField] TMP_InputField _languageInputField;
        [SerializeField] TMP_InputField _sexInputField;
        [SerializeField] TMP_InputField _ageInputField;
        [SerializeField] TMP_InputField _careerInputField;
        [SerializeField] TMP_InputField _educationLevelInputField;
        [SerializeField] GameObject     _errorTip;
        public event Action<int>        OnLogin;

        public void LoginClick()
        {
            if (string.IsNullOrWhiteSpace(_userNameInputField.text) ||
                string.IsNullOrWhiteSpace(_languageInputField.text) ||
                string.IsNullOrWhiteSpace(_sexInputField.text)      ||
                string.IsNullOrWhiteSpace(_ageInputField.text)      ||
                string.IsNullOrWhiteSpace(_careerInputField.text)   ||
                string.IsNullOrWhiteSpace(_educationLevelInputField.text))
            {
                _errorTip.SetActive(true);
                return;
            }

            //TODO 发送信息到服务器
            UserInfoDto userInfoDto = new()
            {
                    UserNameInputField       = _userNameInputField.text,
                    LanguageInputField       = _languageInputField.text,
                    SexInputField            = _sexInputField.text,
                    AgeInputField            = _ageInputField.text,
                    CareerInputField         = _careerInputField.text,
                    EducationLevelInputField = _educationLevelInputField.text,
            };
            OnLogin?.Invoke(userInfoDto.ID);
            Destroy(gameObject);
        }
    }
}