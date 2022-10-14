using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceEmotion : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    private float A=0.0f;
    private float M=0.0f;
    private float speed = 10f;
    private int currentstate=0;

    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    void Update()
    {
        currentstate = DataStructs.lipMotionData.state;
        if (DataStructs.lipMotionData.state == 1)
        {
            closeMouth();
        }
        else if (DataStructs.lipMotionData.state == 2)
        {
            semiOpenMouth();
        }
        else if (DataStructs.lipMotionData.state == 3)
        {
            wideOpenMouth();
        }
    }
    void closeMouth()
    {
        A = meshRenderer.GetBlendShapeWeight(10);
        M = meshRenderer.GetBlendShapeWeight(11);
        A = Mathf.Lerp(A, 0.0f, speed * Time.deltaTime);
        M = Mathf.Lerp(M, 100.0f, speed * Time.deltaTime);
        meshRenderer.SetBlendShapeWeight(10, A);
        meshRenderer.SetBlendShapeWeight(11, M);
    }
    void wideOpenMouth()
    {
        A = meshRenderer.GetBlendShapeWeight(10);
        M = meshRenderer.GetBlendShapeWeight(11);
        A = Mathf.Lerp(A, 100.0f, speed * Time.deltaTime);
        M = Mathf.Lerp(M, 0.0f, speed * Time.deltaTime);
        meshRenderer.SetBlendShapeWeight(10, A);
        meshRenderer.SetBlendShapeWeight(11, M);
    }
    void semiOpenMouth()
    {
        A = meshRenderer.GetBlendShapeWeight(10);
        M = meshRenderer.GetBlendShapeWeight(11);
        A = Mathf.Lerp(A, 50.0f, speed * Time.deltaTime);
        M = Mathf.Lerp(M, 100.0f, speed * Time.deltaTime);
        meshRenderer.SetBlendShapeWeight(10, A);
        meshRenderer.SetBlendShapeWeight(11, M);
    }
}
