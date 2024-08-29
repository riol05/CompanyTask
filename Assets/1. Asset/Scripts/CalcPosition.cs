using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcPosition
{
    public Vector3 objectPos;
    public int maxValue = 0;

    public float x = 0;
    public float y = 0;
    public float z = 0;

    public CalcPosition(int maxVal)
    {
        maxValue = maxVal;
    }
    public Vector3 SetBillPos()
    {
        if (maxValue != 0)
        {

        }
        return objectPos;
    }
    public Vector3 SetBreadPos()
    {
        if(maxValue != 0)
        {

        }
        return objectPos;
    }
}
