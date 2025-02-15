using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnoCardsClient.Statics;

namespace UnoCardsClient.MainPage
{
    public class LoginButtonMainPage : MonoBehaviour
    {
        private Button _loginButtonMainPage;
        // private RectTransform _registerButtonMainPageTransform;
        
        void Start()
        {
            _loginButtonMainPage = GetComponent<Button>();
            _loginButtonMainPage.onClick.AddListener(BtnOnClickFunc);
            // _registerButtonMainPageTransform = GetComponent<RectTransform>();
        }

        void BtnOnClickFunc()
        {
            StaticVariables.MainPageLoginButtonOnClick = true;
            // StaticFunctions.Hide(_registerButtonMainPageTransform);
            SceneManager.LoadScene("Login");
        }
    }
}