using System.Collections;
using System.Collections.Generic;
using Statics;
using TMPro;
using UnityEngine;
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
            
        }

        void OnButtonClick()
        {
            if (_pwd.text != _pwdConfirm.text)
            {
                StaticFunctions.Show(_instructionTextTransform);
                _pwd.text = "";
                _pwdConfirm.text = "";
                return;
            }

            StaticVariables.Registering = true;
        }
    }
}