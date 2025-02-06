using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Statics;

namespace UnoCardsClient.MainPage
{
    public class RegisterButtonMainPage : MonoBehaviour
    {
        private Button _registerButtonMainPage;
        public static bool BtnOnClick;
        // private RectTransform _registerButtonMainPageTransform;
        
        void Start()
        {
            _registerButtonMainPage = GetComponent<Button>();
            _registerButtonMainPage.onClick.AddListener(BtnOnClickFunc);
            // _registerButtonMainPageTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            BtnOnClick = false;
        }

        void BtnOnClickFunc()
        {
            BtnOnClick = true;
            // StaticFunctions.Hide(_registerButtonMainPageTransform);
            SceneManager.LoadScene("Register");
        }
    }
}