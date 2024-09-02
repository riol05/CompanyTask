using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class POS : Area
{
    public Transform bagParent;

    public Transform laneForPay; // 계산줄
    public Transform laneForCafe; // 카페줄
    public BillArea billArea;
    public PaperBag bag;

    private List<Customer> cafeCList;
    private List<Customer> payCList;
    private List<Vector3> cafeLaneVec;
    private List<Vector3> payLaneVec;

    public Area nextAreaIsCafe; // 다음 행선지 카페// 나머지 한곳은 Exit

    public int breadCost;

    public float z;
    private float taskNow = 0;

    private void Awake()
    {
        cafeCList = new List<Customer>();
        payCList = new List<Customer>();
        cafeLaneVec = new List<Vector3>() ;
        payLaneVec = new List<Vector3>() ;

}
public override void Start()
    {
        base.Start();
        CalcPosition payCalc = new CalcPosition(8,laneForPay.position ,0 ,0 ,z);
        CalcPosition cafeCalc = new CalcPosition(8, laneForCafe.position, 0, 0, z);
        cafeLaneVec = cafeCalc.SetCustomerLane();
        payLaneVec = payCalc.SetCustomerLane();
    }

    public override void ArrowActive(bool ison)
    {
        base.ArrowActive(ison);
    }

    public void InteractArea(int i)
    {
        billArea.posArea.InteractArea(breadCost * i);
        SoundManager.instance.PlaySound("Cash");
    }

    public void WaitForPlayer(Customer cust,bool isTakeOut)
    {
        base.WaitForPlayer();
        cust.isWaiting = true;

        if (isTakeOut)
        {
            payCList.Add(cust);
            cust.Move(payLaneVec[payCList.Count - 1]);
        }
        else
        {
            cafeCList.Add(cust);
            cust.Move(cafeLaneVec[cafeCList.Count - 1]);
        }
        cust.isWaiting = true;
    }
    public override void MoveNextArea(Customer cust)
    {
        cust.TaskOver(nextArea);
    }
    private void ResetCafePosition()
    {
        int j = 0;
        if (cafeCList.Count > 0)
        {
            foreach (Customer cust in laneForCafe)
            {
                cust.Move(cafeLaneVec[j]);
                ++j;
            }
        }
    }
    private void ResetPayPosition()
    { 
        int j = 0;
        if (payCList.Count > 0)
        {
            foreach (Customer cust in laneForPay)
            {
                cust.Move(cafeLaneVec[j]);
                ++j;
            }
        }
    }

    public void ManagedByPlayer(Pocket pock)
    {
        if (payCList.Count + cafeCList.Count <= 0) return;
        if (pock.isTask) return;

        
        base.ManagedByPlayer();
        pock.isTask = true;
        StartCoroutine(ManagementCustomers(pock));
    }
    IEnumerator ManagementCustomers(Pocket pock)
    {
        int i = payCList.Count;
        if (i > 0)
        {
            Customer cust = payCList[0];
            PaperBag bag = SpawnManager.Instance.Spawnpaperbag(bagParent, bagParent.position);
            
            int k = cust.curBreadCount;

            for (int j = 0; j < k; j++)
            {
                cust.PutInBag(bagParent.transform.position);
                SoundManager.instance.PlaySound("PutObject");
                yield return new WaitForSeconds(0.5f);
            }
            SoundManager.instance.PlaySound("Cost");
            bag.transform.DOJump(cust.breadPos.position, 2f, 1, 0.3f)
                .OnComplete(() =>{
                    Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
                    bag.transform.localRotation = targetRotation;

                    bag.transform.parent = cust.breadPos;
                    cust.GetBag(bag);
                });

            yield return new WaitForSeconds(0.3f);

            bag.CloseBag();

            MoveNextArea(cust);
            InteractArea(k);
            payCList.Remove(cust);
            ResetPayPosition();
            yield return new WaitForSeconds(0.5f);
        }

        i = cafeCList.Count;
        if (i > 0)
        {
            for(int j = 0; j < cafeCList.Count; j++ )
            {
                cafeCList[j].TaskOver(nextAreaIsCafe);
                cafeCList.Remove(cafeCList[j]);
                yield return new WaitForSeconds(0.3f);
                ResetPayPosition();
            }
        }
        yield return null;

        pock.isTask = false;
    }
}