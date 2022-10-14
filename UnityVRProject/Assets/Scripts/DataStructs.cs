using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DataStructs
{
    public struct partialTrackingStruct
    {
        public float positX;
        public float positY;
        public float positZ;
        public float rotatX;
        public float rotatY;
        public float rotatZ;
    }

    public static partialTrackingStruct partialTrackingData = default;

    public struct LipMotionStruct
    {
        public int state;
    }
    public static LipMotionStruct lipMotionData = default;
}
