using Oculus.Interaction.PoseDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PhoneTransform : MonoBehaviour
{
    public Transform phoneTransform;
    public Transform VRCamera;
    public Transform handTransform;
    private Vector3 transformVector;
    private Vector3 rotationVector;
    private Vector3 InHandPhoneVector;
    private Vector3 reviseVector;
    [SerializeField] TCP_SERVER server;
    [SerializeField] Text debugText;

    void Update()
    {
        InHandPhoneVector = handTransform.position - new Vector3(0.05f, -0.07f, 0.1f);
        if (server.isTracking)
        {
            transformVector.x = VRCamera.position.x + DataStructs.partialTrackingData.positX;
            transformVector.y = VRCamera.position.y + DataStructs.partialTrackingData.positY;
            transformVector.z = VRCamera.position.z + DataStructs.partialTrackingData.positZ;

            phoneTransform.position = (transformVector - reviseVector);

            if ((phoneTransform.position - InHandPhoneVector).magnitude > 0.3f)
                reviseVector = transformVector - InHandPhoneVector;


            debugText.text = "\nphonetransform"
                                    + "\nposition"
                                    + "\nx : " + phoneTransform.position.x.ToString()
                                    + "\ny : " + phoneTransform.position.y.ToString()
                                    + "\nz : " + phoneTransform.position.z.ToString();

            rotationVector.x = DataStructs.partialTrackingData.rotatX;
            rotationVector.y = DataStructs.partialTrackingData.rotatZ; //y and z is changed
            rotationVector.z = DataStructs.partialTrackingData.rotatY;
            rotationVector.x *= -1.0f;
            rotationVector.y *= -1.0f;

            phoneTransform.rotation = Quaternion.Euler(rotationVector+new Vector3(30f,0f,0f));
            debugText.text += "\nrotation"
                                + "\nx : " + rotationVector.x.ToString()
                                + "\ny : " + rotationVector.y.ToString()
                                + "\nz : " + rotationVector.z.ToString();
        }



    }
}
