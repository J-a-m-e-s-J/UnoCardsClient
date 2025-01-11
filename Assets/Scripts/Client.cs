using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour
{
    private Socket _socket;
    private byte[] _buffer = new byte[1024];
    
    // Start is called before the first frame update
    void Start()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Connect("127.0.0.1", 25565);
        StartReceive();
    }

    // Update is called once per frame
    void Update()
    {
        Send();
    }

    void StartReceive()
    {
        _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveCallback, null);
    }

    void RecieveCallback(IAsyncResult iar)
    {
        int len = _socket.EndReceive(iar);
        if (len == 0) return;
        string message = Encoding.UTF8.GetString(_buffer, 0, len);
        Debug.Log(message);
        StartReceive();
    }

    void Send()
    {
        _socket.Send(Encoding.UTF8.GetBytes("hello"), SocketFlags.None);
    }
}
