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
using UnityEngine.UIElements;

public class TCP_SERVER : MonoBehaviour
{
    private static TCP_SERVER instance = null;

    TcpListener serverSocket;
    TcpClient clientSocket;
    NetworkStream clientStream;
    int counter = 0;
    [SerializeField] int port;
    [SerializeField] Text debugText;
    [SerializeField] Text debugText2;
    bool isConnected = false;
    public bool isTracking { get; set; } = false;
    float fpsCounter = 1;
    int prevCounter = 0;
    int currentFPS = 0;
    private Thread acceptThread, readClientThread;

    public static TCP_SERVER GetInstance() => instance;

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


    // Start is called before the first frame update
    void Start()
    {
        serverSocket = new TcpListener(port);
        clientSocket = default(TcpClient);
        serverSocket.Start();
        Debug.Log("Server Started");
        counter = 0;
        acceptThread = new Thread(AcceptClient);
        acceptThread.Start();
        readClientThread = new Thread(ReadDataFromClient);
        readClientThread.Start();
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                if (debugText.text.CompareTo("AR Connected") != 0)
                    debugText.text = "AR waiting" + "\n\nIP is: " + ip.ToString();
            }
        }
    }
    // Update is called once per frame
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
            debugText.text = "AR Connected";
        }

        if (fpsCounter <= 0)
        {
            currentFPS = (counter - prevCounter);
            debugText2.text = "\nFPS : " + currentFPS.ToString();
            prevCounter = counter;
            fpsCounter = 1;
        }
        else
        {
            debugText2.text = "\nFPS : " + currentFPS.ToString();
            fpsCounter -= Time.deltaTime;
        }
    }

    void AcceptClient()
    {
        clientSocket = serverSocket.AcceptTcpClient();
        if (clientSocket != null)
        {
            Debug.Log("AR Connected");
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
                isTracking = true;
                byte[] ba_dataLength = new byte[sizeof(int)];
                int dataLength = 0;
                clientStream.Read(ba_dataLength, 0, ba_dataLength.Length);
                dataLength=BitConverter.ToInt32(ba_dataLength,0);

                byte[] ba_data=new byte[dataLength]; 
                clientStream.Read(ba_data, 0, ba_data.Length);
                
                DataStructs.partialTrackingData.positX = BitConverter.ToSingle(ba_data, 0);
                DataStructs.partialTrackingData.positY = BitConverter.ToSingle(ba_data, 4);
                DataStructs.partialTrackingData.positZ = BitConverter.ToSingle(ba_data, 8);
                DataStructs.partialTrackingData.rotatX = BitConverter.ToSingle(ba_data, 12);
                DataStructs.partialTrackingData.rotatY = BitConverter.ToSingle(ba_data, 16);
                DataStructs.partialTrackingData.rotatZ = BitConverter.ToSingle(ba_data, 20);

                counter += 1;
                clientStream.Flush();
            }
        }
    }
}
