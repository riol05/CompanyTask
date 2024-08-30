using System.Collections.Generic;
using UnityEngine;

public class CalcPosition
{
    public Vector3 objectPos;
    public int maxValue = 0;

    public float x = 0;
    public float y = 0;
    public float z = 0;



    public CalcPosition(int maxVal,Vector3 dir , float y)
    {
        maxValue = maxVal;
        objectPos = dir;
        this.y = y;
    }
    public CalcPosition(int maxVal,Vector3 dir,float x, float y, float z)
    {
        maxValue = maxVal;
        objectPos = dir;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 resetDir(Vector3 dir) => dir = Vector3.zero;




    public List<Vector3> SetBillPos(int curCount,int plusCount)
    {
        List<Vector3> list = new List<Vector3>();

        if (curCount >= maxValue) // 중복 코드 확인
            return null;
        Vector3 targetDir = objectPos;

        for (int i = 0; i < maxValue /6 + 1; i++)
        {
            targetDir.y += y;
            for (int j = 0; j < 3; j++)
            {
                targetDir.z += z;
                for (int k = 0; k < 2; k++)
                {
                    targetDir.x += x;
                    list.Add(targetDir);
                }
                targetDir.x = objectPos.x;
            }
            targetDir.z = objectPos.z;
        }

        if (curCount != 0)
            list.RemoveRange(0, curCount); // 중복된 앞쪽 요소 제거

        if (list.Count > plusCount )
            list.RemoveRange(plusCount, list.Count - plusCount); // 포함되지 않은 뒤쪽 요소 제거


        return list;
    }


    public List<Vector3> SetBreadPos(int curCount, int count)
    {
        List<Vector3> list = new List<Vector3>();

        if (curCount >= maxValue)
            return null;

        for (int i = 0; i < maxValue; i++ )
        {
            objectPos.y = objectPos.y + y* curCount;
            list.Add(objectPos);
        }

        return list;
    }

    public List<Vector3> SetBasket(int curCount)
    {
        List<Vector3> list = new List<Vector3>();

        if (curCount >= maxValue)
            return null;

        Vector3 targetDir = objectPos;


        for (int j = 0; j < 4; j++)
        {
            objectPos.z += z;
            for (int k = 0; k < 2; k++)
            {
                objectPos.x += x;
                list.Add(objectPos);
            }
            targetDir = objectPos;
        }
        
        if (curCount != 0)// 중복 제외
        {
            for (int i = curCount; i < maxValue; i++)
            {
                list.RemoveAt(i);
            }
        }

        return list;
    }
    public Vector3 SetCustomerLane(int t)
    {
        Vector3 dir = Vector3.zero;
        float y = -1.5f;

        return dir;
    }
}
