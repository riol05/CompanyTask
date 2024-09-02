using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETableState
{
    Normal,
    Using,
    NeedClean
}
public class Cafe : Area
{
    public ETableState state;

    public Transform activeArea;
    public Transform inActiveArea;
    public Taking takingArea;

    public Transform lanePos;
    public Transform chair;
    public Transform trash;
    public Transform breadPos;

    public ParticleSystem cleanParticle;
    public ParticleSystem openParticle;

    public int areaCost;
    public int breadCost;
    public Area posArea;
    
    private bool isOpen = false;
    private List<Customer> custList;
    private Customer curCustomer;
    private List<Vector3> lanePosList;
    private float taskTime;

    public override void Start()
    {
        base.Start();

        state = ETableState.Normal;
        isOpen = false;

        custList = new List<Customer>();
    }

    private void OnEnable()
    {
        CalcPosition calc = new CalcPosition(maxValue, lanePos.position, 1.1f);
        lanePosList = calc.SetCustomerLane();

        cleanParticle.Stop();
        openParticle.Stop();
    }

    private void FixedUpdate()
    {
        taskTime += Time.deltaTime;
        if (isOpen)
        {
            if (taskTime < 2)
                return;
            else
            {
                taskTime = 0;
                if (custList.Count > 0)
                    curCustomer = custList[0];

                InteractArea();
            }
        }
    }

    public override void InteractArea()
    {
        if (isOpen)
        {
            if (custList.Count < 1) return;

            if (state == ETableState.Normal)
                StartCoroutine(TalkingInCafe());
        }
    }

    public override void ArrowActive(bool isOn)
    {
        base.ArrowActive(isOn);
    }
    public override void MoveNextArea(Customer cust)
    {
        cust.TaskOver(nextArea);
        
    }
    public void ManagedByPlayer(Pocket pocket)
    {
        base.ManagedByPlayer();
        if(isOpen)
        {
            if (state == ETableState.NeedClean)
            {
                cleanParticle.Play();
                state = ETableState.Normal;
                SoundManager.instance.PlaySound("Trash");
                trash.gameObject.SetActive(false);
            }
        }
        else
        {
            if(pocket.billAmount >= areaCost)
            {
                activeArea.gameObject.SetActive(true);
                inActiveArea.gameObject.SetActive(false);

                pocket.calculateBills(-areaCost);
                isOpen = true;
                
                openParticle.Play();
                SoundManager.instance.PlaySound("Success");
            }
        }
    }

    public void UsedTable()
    {
        MoveNextArea(curCustomer);
        takingArea.InteractArea(breadCost);

        trash.gameObject.SetActive(true);
        state = ETableState.NeedClean;
    }

    public void WaitForService(Customer cust)
    {
        if(custList.Count >= 1)
        {
            cust.ChangeTakeOut();
            cust.TaskOver(posArea);
            return;
        }
        if (!cust.isWaiting)
        {
            custList.Add(cust);
            cust.isWaiting = true;
        }
    }

    IEnumerator TalkingInCafe()
    {
       
        state = ETableState.Using;
        
        curCustomer = custList[0];
        Bread crois = curCustomer.PutBread(breadPos);
        custList.RemoveAt(0);
        
        curCustomer.Move(chair.position);

        curCustomer.isTalking = true;
        yield return new WaitForSeconds(5);

        SpawnManager.Instance.DespawnBreads(crois);
        curCustomer.isTalking = false;
        
        yield return null;

        UsedTable();
    }
}
