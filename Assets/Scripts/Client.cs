using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using UnoCardsClient.MainPage;
using UnoCardsClient.Register;
using Statics;

namespace UnoCardsClient.Client
{
    public class Client : MonoBehaviour
    {
        private byte[] _buffer = new byte[1024];
        private GameObject _gameObjectTemp;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!StaticVariables.GameRunning)
            {
                if (SceneManager.GetActiveScene().name != "Main")
                {
                    SceneManager.LoadScene("Main");
                }
                StaticVariables.Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                StaticVariables.Client.Connect("127.0.0.1", 25565);
                StaticVariables.ClientEndPoint = StaticVariables.Client.LocalEndPoint as IPEndPoint;
                StaticVariables.GameRunning = true;
                StartReceive();
            }
        }

        // Update is called once per frame
        void Update()
        {
            GetActiveSceneName();
            switch (StaticVariables.ActiveSceneName)
            {
                case "Main":
                    if (StaticVariables.MainPageRegisterButtonOnClick)
                    {
                        // Send语法: 执行任务名-(相关参数(空格分隔，若无参数则写一对空括号))-客户端ip
                        // 函数：
                        // exit-() -> 退出服务端
                        // log-(字符串) -> 输出
                        // sqlite-(操作 相关参数) -> 操作：insert(username, password), update(username/password, val_username/val_password, new_password/new_username)
                        // client-(操作 相关参数) -> 操作：exit()
                        SendMsg($"log-({StaticVariables.ClientEndPoint.Address} entering register page)");
                    }
                    break;
                
                case "Register":
                    if (StaticVariables.Registering)
                    {
                        SendMsg($"sqlite-(insert {StaticVariables.CurrentUsername} {StaticVariables.CurrentPassword})-{StaticVariables.ClientEndPoint.Address}-{StaticVariables.ClientEndPoint.Port}");
                        StaticVariables.Registering = false;
                    }
                    break;
            }
            // Debug.Log(1);
        }

        void OnApplicationQuit()
        {
            StaticVariables.Client.Disconnect(false);
            StaticVariables.Client.Close();
        }

        void StartReceive()
        {
            StaticVariables.Client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveCallback, null);
        }

        void RecieveCallback(IAsyncResult iar)
        {
            int len = StaticVariables.Client.EndReceive(iar);
            if (len == 0) return;
            string message = Encoding.UTF8.GetString(_buffer, 0, len);
            // Debug.Log(message);
            HandleMessage(message);
            // Debug.Log(StaticVariables.RegisterStatus);
            // Debug.Log(StaticVariables.RegisterStatusReceived);
            StartReceive();
        }

        void HandleMessage(string message)
        {
            // Debug.Log(message);
            switch (StaticVariables.ActiveSceneName)
            {
                case "Register":
                    StaticVariables.RegisterStatus = message!;
                    StaticVariables.RegisterStatusReceived = true;
                    // Debug.Log(StaticVariables.RegisterStatus);
                    // Debug.Log(StaticVariables.RegisterStatusReceived);
                    break;
            }
        }

        void SendMsg(string message)
        {
            StaticVariables.Client.Send(Encoding.UTF8.GetBytes(message));
        }

        void GetActiveSceneName()
        {
            StaticVariables.ActiveSceneName = SceneManager.GetActiveScene().name;
        }
    }
}