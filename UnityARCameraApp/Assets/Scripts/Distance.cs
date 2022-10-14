using System;
using UnityEngine;
using UnityEngine.UI;
public class Distance : MonoBehaviour
{
    [SerializeField] private GameObject ARcamera;
    [SerializeField] private GameObject ModelObject;
    [SerializeField] private Text debugText;
    [SerializeField] private Text distanceText;
    private Vector3 positionVector;
    private Vector3 rotationVector;
    private float[] phonetransform;
    public bool targetOn { get; set; }=false;
    TCP_Client2 client;

    private void Start()
    {
        client=TCP_Client2.GetInstance();
    }
    void Update()
    {
        if(ARcamera == null)
            return;
        if (ModelObject == null)
            return;
        if (targetOn)
        {
            positionVector = ARcamera.transform.position - ModelObject.transform.position;
            rotationVector = ModelObject.transform.rotation.eulerAngles;
            distanceText.text = "position\n"
                                + "x: " + positionVector.x.ToString() + "\n"
                                + "y: " + positionVector.y.ToString() + "\n"
                                + "z: " + positionVector.z.ToString() + "\n"
                                + "rotation\n"
                                + "x: " + rotationVector.x.ToString() + "\n"
                                + "y: " + rotationVector.y.ToString() + "\n"
                                + "z: " + rotationVector.z.ToString();
            debugText.text = positionVector.magnitude.ToString();
            if (client.isConnected())
            {
                phonetransform = new float[6];
                phonetransform[0] = positionVector.x;
                phonetransform[1] = positionVector.y;
                phonetransform[2] = positionVector.z;
                phonetransform[3] = rotationVector.x;
                phonetransform[4] = rotationVector.y;
                phonetransform[5] = rotationVector.z;
                byte[] byteArray = new byte[sizeof(float) * phonetransform.Length];
                for (int i=0;i<phonetransform.Length;i++)
                {
                    byte[] temp = BitConverter.GetBytes(phonetransform[i]);
                    for (int j = 0; j < sizeof(float); j++)
                    {
                        byteArray[sizeof(float)*i+ j] = temp[j];
                    }
                }
                client.Send(byteArray);
            }
        }
    }
}
