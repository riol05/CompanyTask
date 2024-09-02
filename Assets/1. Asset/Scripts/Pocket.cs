using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask areaMask;
    PlayerMove plMove;
    public Transform handPos;
    public Transform maxText;

    public List<Bread> breadList;
    public int maxValue;

    public int breadAmount;
    public int billAmount;

    public float breadHeight; // 0.4f

    private float areaCoolDown;
    private bool isStack;

    public bool isTask;
    private bool ismax;

    private void Awake()
    {
        plMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        breadList = new List<Bread>();
        maxText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(breadList.Count == 0 &&
            isStack)
        {
            isStack = false;
            plMove.ChangeStackingState(isStack);
        }

        areaCoolDown += Time.fixedDeltaTime;

        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+1.5f,transform.position.z),
            transform.forward , out hit , 1.5f , areaMask))
        {
            if (areaCoolDown > 1f)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);

                if (distance < 4f)
                {
                    areaCoolDown = 0;

                    if (hit.collider.GetComponent<Taking>())
                    {
                        hit.collider.GetComponent<Taking>().ManagedByPlayer(this);
                    }
                    else if (hit.collider.GetComponent<Basket>())
                    {
                        hit.collider.GetComponent<Basket>().ManagedByPlayer(this, breadList,isStack);
                    }
                    else if (hit.collider.GetComponent<Oven>())
                    {
                        hit.collider.GetComponent<Oven>().ManagedByPlayer(this);
                    }
                    else if( hit.collider.GetComponent<POS>())
                    {
                        hit.collider.GetComponent<POS>().ManagedByPlayer(this);
                    }

                    if (hit.collider.GetComponent<Cafe>())
                    {
                        hit.collider.GetComponent<Cafe>().ManagedByPlayer(this);
                        areaCoolDown = 0;
                    }
                }
            }
        }
    }

    public void calculateBills(int i)
    {
        billAmount += i;
        GameManager.Instance.GetMoney(billAmount);
    }

    public void StackingOnHand(int ovenAmount, List<Bread> crois, bool isFilled)
    {
        if(breadAmount >= maxValue)
        {
            ismax = true;
            maxText.gameObject.SetActive(true);

            breadAmount = maxValue;
            return;
        }
        if (breadAmount < maxValue)
        {
            ismax = false;
            maxText.gameObject.SetActive(false);
        }
        int needAmount = maxValue - breadAmount;
        
        int takeAmount = ovenAmount - needAmount < 0 ? ovenAmount : needAmount;

        StartCoroutine(SetOnCroisRoutine(takeAmount, crois, isFilled));
    }

    IEnumerator SetOnCroisRoutine(int takeAmount, List<Bread> crois, bool isfilled)
    {
        if (breadList.Count == 0 && isStack)
        {
            isStack = false;
            plMove.ChangeStackingState(isStack);
        }
        CalcPosition calc = new CalcPosition(maxValue, handPos.position, breadHeight);
        List<Vector3> breadPosList = calc.SetBreadPos(breadAmount, takeAmount);

        isStack = true;
        plMove.ChangeStackingState(isStack);// stacking animation 관련 함수 TODO : 다시 false 해줄것
        


        int i = 0;
        for (int j = breadAmount; j < takeAmount + breadAmount; j++)
        {
            Bread bread = crois[0];
            crois.Remove(bread);
            breadList.Add(bread);
            bread.GetFromOven(breadPosList[i], handPos);
            yield return new WaitForSeconds(0.3f);
            i++;
        }
        breadAmount += takeAmount;



        if (breadAmount >= maxValue)
        {
            ismax = true;
            maxText.gameObject.SetActive(true);
        }
        if (breadAmount < maxValue)
        {
            ismax = false;
            maxText.gameObject.SetActive(false);
        }
        if (crois.Count > 0)
        {
            isfilled = true;
        }
    }
}
