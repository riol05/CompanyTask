using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Basket : Area
{
    public Transform breadPos;
    private List<Bread> breadList;
    private List<Customer> customerList;
    public float x;
    public float z;
    private float taskNow = 0;
    private void Awake()
    {
        breadList = new List<Bread>();
        customerList = new List<Customer>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void ArrowActive(bool ison)
    {
        base.ArrowActive(ison);
    }

    private void FixedUpdate()
    {
        taskNow += Time.deltaTime;
        if (breadList.Count == 0) isFilled = false;

        if (customerList.Count > 0)
        {
            if (customerList[0].CanGo())
            {
                MoveNextArea(customerList[0]);

                if (breadList.Count > 0) isFilled = true;

                else isFilled = false;
            }
        }

        if (isFilled)
        {
            if (taskNow < 2)
                return;

            else
            {
                taskNow = 0;
                if (customerList.Count > 0)
                {
                    if (breadList.Count > 0)
                    {
                        isFilled = false;
                        taskNow = 0;
                        customerList[0].GetBread(breadList);
                    }
                }
            }
        }

    }

    public void InteractArea(Customer cust)
    {
        curAmount = breadList.Count;
        if (cust.isWaiting) return;
        customerList.Add(cust);
        cust.isWaiting = true;

        if (curAmount == 0)
            return;
    }

    public override void MoveNextArea(Customer cust)
    {
        customerList.Remove(cust);
        cust.TaskOver(nextArea);
    }

    public void ManagedByPlayer(Pocket pocket, List<Bread> croisList, bool isStack)
    {

        curAmount = breadList.Count;
        if (curAmount >= maxValue)
        {
            return;
        }
        CalcPosition calc = new CalcPosition(maxValue, breadPos.position, x, 0, z);
        List<Vector3> vecList = calc.SetBasket(pocket.breadAmount, curAmount);

        if (vecList == null)
            return;

        pocket.breadAmount = BreadCount(pocket.breadAmount, vecList.Count);
        if(pocket.breadAmount == 0)
        { isStack = false; }
        StartCoroutine(SetBreadRoutine(vecList,croisList,isStack));
    }

    IEnumerator SetBreadRoutine(List<Vector3> vecList, List<Bread> croisList,bool isStack)
    {
        int i = 0;
        for (int j = 0; j < vecList.Count; j++)
        {
            Bread bread = croisList[croisList.Count - 1];
            croisList.Remove(bread);
            breadList.Add(bread);
            bread.PutOnPosition(vecList[i], breadPos);
            yield return new WaitForSeconds(0.3f);
            i++;
        }
        yield return new WaitForSeconds(0.3f);
        
        isFilled = true;
        yield return null;
    }
    public override void WaitForPlayer()
    {
        base.WaitForPlayer();
    }

    private int BreadCount(int bCount, int vCount)
    {
        bCount -= vCount;
        return bCount;
    }
}
