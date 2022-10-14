using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class TCP_Client2 : MonoBehaviour
{
    public InputField IPInput, PortInput;
    public string serverIP;
    public int serverPort;

    private bool socketReady;
    TcpClient socket;
    private NetworkStream stream = null;
    private StreamWriter writer;
    private StreamReader reader;

    private byte[] dataLen;
    public SceneChangeManager sceneChangeManager;
    private static TCP_Client2 instance;
    public static TCP_Client2 GetInstance() => instance;

    public bool isConnected() => socketReady;

    private void OnEnable()
    {
        if (TCP_Client2.instance == null)
        {
            TCP_Client2.instance = this;
        }
        else if (TCP_Client2.instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        Time.fixedDeltaTime = 1f / 60f;
    }

    void Update()
    {
        if (stream == null)
            return;
        if (stream.DataAvailable)
        {
            sceneChangeManager.isClient2Connect = true;
        }
    }

    public void ConnectToServer()
    {
        if (socketReady) return;

        string ip;
        int port;
        if (IPInput != null)
        {
            ip = IPInput.text == "" ? serverIP : IPInput.text;
        }
        else
        {
            ip = serverIP;
        }
        if (PortInput != null)
        {
            port = PortInput.text == "" ? serverPort : int.Parse(PortInput.text);
        }
        else
        {
            port = serverPort;
        }

        try
        {
            socket = new TcpClient(ip, port);
            socket.SendBufferSize = 12000000;
            stream = socket.GetStream();
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error : " + e.Message);
        }
        Debug.Log("Connected to Server");

        sceneChangeManager.isClient2Connect = true;
    }
    public void Send(byte[] bytes = null)
    {
        if (!socketReady) return;
        if (!stream.CanWrite) return;

        dataLen = BitConverter.GetBytes(bytes.Length);

        if (bytes == null)
        {
            return;
        }
        stream.WriteAsync(dataLen, 0, dataLen.Length);
        stream.WriteAsync(bytes, 0, bytes.Length);
        stream.FlushAsync();
    }
    void OnApplicationQuit()
    {
        CloseSocket();
    }
    void CloseSocket()
    {
        if (!socketReady) return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
}
