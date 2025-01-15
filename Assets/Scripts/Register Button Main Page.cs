using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnoCardsClient.MainPage
{
    public class RegisterButtonMainPage : MonoBehaviour
    {
        private Button _registerButtonMainPage;
        public static bool BtnOnClick = false;
        
        void Start()
        {
            _registerButtonMainPage = GetComponent<Button>();
            _registerButtonMainPage.onClick.AddListener(BtnOnClickFunc);
        }

        private void Update()
        {
            BtnOnClick = false;
        }

        void BtnOnClickFunc()
        {
            BtnOnClick = true;
            SceneManager.LoadScene("Register");
        }
    }
}