using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public Bill billPrefab;

    public Bread breadPrefab;

    public Transform handPos;

    public List<Bread> breadList;
    public int maxValue;

    private int breadInteger;
    public int billInteger;

    public float breadHeight; // 0.4f

    public void calculateBills(int i)
    {
        billInteger += i;
    }

    public void InActiveBread(int i)
    {
        breadInteger += i;
    }

    public void StackingOnHand(int ovenAmount, List<Bread> crois)
    {
        List<Vector3> breadPosList = new List<Vector3>();

        CalcPosition calc = new CalcPosition(maxValue, handPos.position, breadHeight);

        breadPosList = calc.SetBreadPos(breadInteger,ovenAmount);

        for (int j = breadInteger; j < maxValue; j++)
        {
            crois[j].GetFromOven(breadPosList[j]);
            breadList.Add(crois[j]);
        }


    }
}
