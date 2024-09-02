using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections;
using TreeEditor;
using DG.Tweening;

public class Taking : MonoBehaviour
{
    public bool isFilled; // Æ©Åä¸®¾ó ¿ë
    
    public float areaRange;
    public int maxValue;
    public Arrow arrow;

    public Transform spawnPos;
    private Vector3 nextSpawnPos;
    private List<Bill> billList;
    private Queue<Bill> billQue;
    private int curBalance;

    public float x; 
    public float y;
    public float z;

    void Start()
    {
        billList = new List<Bill>();
        isFilled = false;
        ArrowActive(isFilled);
    }
    
    //RaycastHit hit;
    //private void FixedUpdate()
    //{
    //    Physics.SphereCast(transform.position, areaRange, transform.forward, out hit);
    //    if(hit.collider.GetComponent<Pocket>())
    //    {
    //        if(isFilled)
    //        {
    //            ManagedByPlayer(hit.collider.GetComponent<Pocket>());
    //        }
    //    }
    //}

    private void ArrowActive(bool ison)
    {
        arrow.gameObject.SetActive(ison);
    }

    public void CheckAreaIsFilled(bool isOn)
    {
        ArrowActive(isOn);
    }

    public void InteractArea(int i)
    {
        if(curBalance + i > maxValue)
        {
            curBalance = maxValue;
            return;
        }
        CalcPosition calc = new CalcPosition(maxValue, transform.position, x, y, z);
        List<Vector3> vecList = new List<Vector3>();

        vecList = calc.SetBillPos(curBalance,i);
        

        foreach (Vector3 dir in vecList)
        {
            if (dir == vecList[vecList.Count - 1])
                nextSpawnPos = dir;

            billList.Add(SpawnManager.Instance.SpawnBill(spawnPos, dir));
        }
        
        isFilled = true;
        ArrowActive(isFilled);
        curBalance += i;
        
    }
    public void ManagedByPlayer(Pocket pocket)
    {
        if (isFilled)
        {
            isFilled = false;
            ArrowActive(isFilled);

            pocket.calculateBills(billList.Count);
            curBalance = 0;
            StartCoroutine(GetMoney(pocket));
        }
    }

    IEnumerator GetMoney(Pocket pocket)
    {
        int j = billList.Count;
        for (int i = j - 1; i >= 0; i--)
        {
            Bill bil = billList[i];
            bil.GetFromGround(pocket.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
        billList.Clear();
    }
}




