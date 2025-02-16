using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnoCardsClient.Statics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnoCardsClient.Login
{
    public class SubmitButtonLogin : MonoBehaviour
    {
        private Button _submitButton;
        private TMP_InputField _userName;
        private TMP_InputField _pwd;
        private TextMeshProUGUI _instructionText;
        private RectTransform _instructionTextTransform;
        private GameObject _gameObjectTemp;
        
        // Start is called before the first frame update
        void Start()
        {
            _submitButton = GetComponent<Button>();
            _submitButton.onClick.AddListener(OnButtonClick);
            
            _gameObjectTemp = GameObject.Find("Input Username");
            _userName = _gameObjectTemp.GetComponent<TMP_InputField>();

            _gameObjectTemp = GameObject.Find("Input Password");
            _pwd = _gameObjectTemp.GetComponent<TMP_InputField>();

            _gameObjectTemp = GameObject.Find("Instruction Text");
            _instructionText = _gameObjectTemp.GetComponent<TextMeshProUGUI>();
            _instructionTextTransform = _gameObjectTemp.GetComponent<RectTransform>();
            StaticFunctions.Hide(_instructionTextTransform);
            
            _gameObjectTemp = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (StaticVariables.LoginStatusReceived)
            {
                switch (StaticVariables.LoginStatus)
                {
                    case "Password contains space":
                        _instructionText.text = "Password contains space!";
                        _pwd.text = "";
                        StaticFunctions.Show(_instructionTextTransform);
                        break;
                    
                    case "Login success":
                        SceneManager.LoadScene("Main Game");
                        break;
                    
                    case "Login failed":
                        _instructionText.text = "Incorrect username or password!";
                        _userName.text = "";
                        _pwd.text = "";
                        StaticFunctions.Show(_instructionTextTransform);
                        break;
                }
                
                StaticVariables.LoginStatusReceived = false;
            }
        }

        void OnButtonClick()
        {
            StaticVariables.CurrentUsername = _userName.text;
            StaticVariables.CurrentPassword = _pwd.text;
            StaticVariables.Logining = true;
        }
    }
}