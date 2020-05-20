using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    public float Min;
    public float Max;

    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float value){
        if (value < Min){
            Min = value;
        }
        if (value > Max){
            Max = value;
        }
    }
}
