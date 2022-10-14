using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.UI;
using System.Data.SqlTypes;

public class TCP_SERVER2 : MonoBehaviour
{
    private static TCP_SERVER2 instance = null;

    TcpListener serverSocket;
    TcpClient clientSocket;
    NetworkStream clientStream;
    [SerializeField] int port;
    [SerializeField] Text debugText;
    bool isConnected = false;
    private Thread acceptThread, readClientThread;

    public static TCP_SERVER2 GetInstance() => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        serverSocket = new TcpListener(port);
        clientSocket = default(TcpClient);
        serverSocket.Start();
        Debug.Log("Server2 Started");
        acceptThread = new Thread(AcceptClient);
        acceptThread.Start();
        readClientThread = new Thread(ReadDataFromClient);
        readClientThread.Start();
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                if (debugText.text.CompareTo("python Client Connected") != 0)
                    debugText.text = "python waiting";
            }
        }

    }

    void Update()
    {
        if (clientSocket == null)
            return;

        if (!clientSocket.Connected)
        {
            return;
        }

        if (isConnected)
        {
            debugText.text = "python Client Connected\n\n";
        }
        if (DataStructs.lipMotionData.state != 0)
        {
            if(DataStructs.lipMotionData.state == 1)
            {
                debugText.text += "closed";
            }
            else if (DataStructs.lipMotionData.state == 2)
            {
                debugText.text += "semiopen";
            }
            else if (DataStructs.lipMotionData.state == 3)
            {
                debugText.text += "wideopen";
            }
            else
            {
                debugText.text += "None";
            }
        }
    }
    void AcceptClient()
    {
        clientSocket = serverSocket.AcceptTcpClient();
        if (clientSocket != null)
        {
            Debug.Log("Python Connected");
            clientSocket.ReceiveBufferSize = 12000000;
            clientStream = clientSocket.GetStream();
            isConnected = true;
        }
    }
    void ReadDataFromClient()
    {
        while (true)
        {
            if (clientSocket == null || clientStream == null)
            {
                continue;
            }
            if (clientStream.DataAvailable)
            {
                byte[] data = new byte[sizeof(int)];
                clientStream.Read(data, 0, data.Length);
                int emotionIndex = BitConverter.ToInt32(data);
                DataStructs.lipMotionData.state = emotionIndex;

                clientStream.Flush();
            }
        }
    }
}
