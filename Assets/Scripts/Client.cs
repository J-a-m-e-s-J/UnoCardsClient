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
using Statics;

namespace UnoCardsClient.Client
{
    public class Client : MonoBehaviour
    {
        private Socket _client;
        private byte[] _buffer = new byte[1024];
        private Button _button;
        private IPEndPoint _clientEndPoint;
        private GameObject _gameObjectTemp;
        
        // Start is called before the first frame update
        void Start()
        {
            // SceneManager.LoadSceneAsync("Main");
            if (SceneManager.GetActiveScene().name != "Main" && !StaticVariables.GameRunning)
            {
                SceneManager.LoadScene("Main");
            }
            StaticVariables.GameRunning = true;
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _client.Connect("127.0.0.1", 25565);
            _clientEndPoint = _client.LocalEndPoint as IPEndPoint;
            // Debug.Log(_clientEndPoint?.Address);
            StartReceive();
        }

        // Update is called once per frame
        void Update()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Main":
                    _gameObjectTemp = GameObject.Find("Register Button");
                    _button = _gameObjectTemp.GetComponent<Button>();
                    if (RegisterButtonMainPage.BtnOnClick)
                    {
                        // Send语法: 执行任务名 (相关参数(空格分隔，若无参数则写一对空括号)) -> 客户端ip
                        // 函数：
                        // exit () -> 退出服务端
                        // log (字符串) -> 输出
                        // sqlite (操作 相关参数) -> 操作：insert(username, password), update(username/password, val_username/val_password, new_password/new_username)
                        // client (操作 相关参数) -> 操作：exit()
                        _client.Send(Encoding.UTF8.GetBytes($"log (\"{_clientEndPoint.Address} entering register page\") -> " + _clientEndPoint.Address));
                        // _client.Send(Encoding.UTF8.GetBytes("exit"));
                    }
                    break;
                
                case "Register":
                    if (StaticVariables.Registering)
                    {
                        _client.Send(Encoding.UTF8.GetBytes($"sqlite ()"));
                    }
                    break;
            }
            // Debug.Log(1);
            _client.Send(Encoding.UTF8.GetBytes("exit"));
        }

        void StartReceive()
        {
            _client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveCallback, null);
        }

        void RecieveCallback(IAsyncResult iar)
        {
            int len = _client.EndReceive(iar);
            if (len == 0) return;
            string message = Encoding.UTF8.GetString(_buffer, 0, len);
            Debug.Log(message);
            StartReceive();
        }

        void Send(string msg)
        {
            _client.Send(Encoding.UTF8.GetBytes(msg));
        }
    }
}