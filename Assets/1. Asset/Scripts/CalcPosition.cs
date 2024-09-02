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



    public List<Vector3> SetBillPos(int curAmount,int plusAmount) // bills Position ��� ���� �Լ�
    {
        List<Vector3> list = new List<Vector3>();

        if (curAmount >= maxValue)
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

        if (curAmount != 0)
            list.RemoveRange(0, curAmount); 

        if (list.Count > plusAmount )
            list.RemoveRange(plusAmount, list.Count - plusAmount);

        return list;
    }


    public List<Vector3> SetBreadPos(int curCount, int count) // Bread Position ��� ���� �Լ�
    {
        List<Vector3> list = new List<Vector3>();

        if (curCount > maxValue)
            return null;

        for (int i = curCount; i < count + curCount; i++ )
        {
            objectPos.y = y* i;
            list.Add(objectPos);
        }

        return list;
    }

    public List<Vector3> SetBasket(int plusAmount,int curAmount) // BreadPosition ��� ���� �Լ� // Basket Ŭ����
    {
        List<Vector3> list = new List<Vector3>();

        if (curAmount >= maxValue)
            return null;

        Vector3 targetDir = objectPos;

        for (int j = 0; j < 5; j++)
        {
            objectPos.z += z * j;
            for (int k = 0;k < 2; k++)
            {
                objectPos.x += x* k;
                list.Add(objectPos);
            }
            objectPos = targetDir;
        }
       
        if (curAmount != 0)
            list.RemoveRange(0, curAmount);

        if (list.Count > plusAmount)
            list.RemoveRange(plusAmount, list.Count - plusAmount);
        return list;
    }
    
    public List<Vector3> SetCustomerLane() // PosArea Customer �� Position ��� ���� �Լ�
    {
        List<Vector3> list = new List<Vector3>();

        for(int i = 0; i < maxValue; i++)
        {
            objectPos.z += z * i;
            list.Add(objectPos);
        }

        return list;
    }
}
