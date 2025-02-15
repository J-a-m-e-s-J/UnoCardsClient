using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnoCardsClient.Statics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnoCardsClient.Register
{
    public class SubmitButtonRegister : MonoBehaviour
    {
        private Button _submitButton;
        private TMP_InputField _userName;
        private TMP_InputField _pwd;
        private TMP_InputField _pwdConfirm;
        private TextMeshProUGUI _instructionText;
        private RectTransform _instructionTextTransform;
        private GameObject _gameObjectTemp;
        
        // Start is called before the first frame update
        void Start()
        {
            _submitButton = GetComponent<Button>();
            
            _gameObjectTemp = GameObject.Find("Input Username");
            _userName = _gameObjectTemp.GetComponent<TMP_InputField>();

            _gameObjectTemp = GameObject.Find("Input Password");
            _pwd = _gameObjectTemp.GetComponent<TMP_InputField>();
            
            _gameObjectTemp = GameObject.Find("Confirm Password");
            _pwdConfirm = _gameObjectTemp.GetComponent<TMP_InputField>();

            _gameObjectTemp = GameObject.Find("Instruction Text");
            _instructionText = _gameObjectTemp.GetComponent<TextMeshProUGUI>();
            _instructionTextTransform = _gameObjectTemp.GetComponent<RectTransform>();
            StaticFunctions.Hide(_instructionTextTransform);
            
            _gameObjectTemp = null;
            
            _submitButton.onClick.AddListener(OnButtonClick);
        }

        // Update is called once per frame
        void Update()
        {
            if (StaticVariables.RegisterStatusReceived)
            {
                switch (StaticVariables.RegisterStatus)
                {
                    case "Username already exists":
                        _instructionText.text = "Username already exists!";
                        _userName.text = "";
                        StaticFunctions.Show(_instructionTextTransform);
                        break;
                    
                    case "Password contains space":
                        _instructionText.text = "Password contains space!";
                        _pwd.text = "";
                        _pwdConfirm.text = "";
                        StaticFunctions.Show(_instructionTextTransform);
                        break;
                    
                    case "Register success":
                        SceneManager.LoadScene("Main Game");
                        break;
                }

                StaticVariables.RegisterStatusReceived = false;
            }
        }

        void OnButtonClick()
        {
            if (_pwd.text != _pwdConfirm.text)
            {
                _instructionText.text = "Passwords do not match!";
                StaticFunctions.Show(_instructionTextTransform);
                _pwd.text = "";
                _pwdConfirm.text = "";
                return;
            }
            
            StaticVariables.CurrentUsername = _userName.text;
            StaticVariables.CurrentPassword = _pwd.text;
            StaticVariables.Registering = true;
        }
    }
}