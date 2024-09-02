using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public enum ECustomer
{
    None,
    Bread,
    Cafe,
    Pay,
    Home
}
public class Customer : MonoBehaviour
{
    public UIState uiState;
    public LayerMask areaLayer;
    public Transform breadPos;
    public float y;
    public ParticleSystem happyEmoji;

    public int curBreadCount;

    private List<Bread> breadList;
    private Area nextArea;
    private NavMeshAgent nmAgent;
    private Animator anim;
    private Vector3 targetPos;
    private PaperBag paperBag;

    private bool isStacking;
    public bool isWaiting;
    public bool isTalking = false;

    private float areaCoolDown = 0;
    private bool complete = false;
    private int maxValue = 3;

    private ECustomer state;
    private RaycastHit hit;

    private bool isTakeOut;
    private int needAmount;
    #region Animation String 용
    string idle;
    string run;
    string stack;
    string talking;

    private void AnimationString()
    {
        idle = "Idle";
        run = "Run";
        stack = "Stack";
        talking = "Talking";
    }

    #endregion
    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        AnimationString();
        breadList = new List<Bread>();
        maxValue = 3;
    }
    private void OnEnable()
    {
        state = ECustomer.Bread;
        ChoiceReset();
    }
    void Update()
    {
        if(breadList.Count > 0) curBreadCount = breadList.Count;

        areaCoolDown += Time.deltaTime;
        #region 애니메이션
        if (!isTalking)
        {
            anim.SetTrigger(nmAgent.isStopped ? idle : run);
        }
        else
            anim.SetTrigger(talking);
        #endregion
        
        if(state == ECustomer.Cafe)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.4)
                nmAgent.isStopped = true;
        }
        else if (state != ECustomer.Pay)
        {
            if (Vector3.Distance(transform.position, targetPos) < 2.5)
                nmAgent.isStopped = true;
        }
        else // calc 줄 서기
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.3)
                nmAgent.isStopped = true;
        }

        #region customer 상호작용
        if (nextArea != null && !isWaiting)
        {
            if (areaCoolDown > 1f)
            {
                float distance = Vector3.Distance(transform.position, nextArea.transform.position);
                if (distance < 2.5)
                {
                    if (nextArea == AreaManager.Instance.basketArea)
                    {
                        areaCoolDown = 0;
                        if (!complete)
                            AreaManager.Instance.basketArea.InteractArea(this);
                    }
                    else if (nextArea == AreaManager.Instance.posArea)
                    {
                        areaCoolDown = 0;
                        AreaManager.Instance.posArea.WaitForPlayer(this, isTakeOut);
                    }
                    else if(nextArea == AreaManager.Instance.cafeArea)
                    {
                        AreaManager.Instance.cafeArea.WaitForService(this);
                    }
                    else if(nextArea == AreaManager.Instance.SpawnArea)
                    {
                        if (isTakeOut)
                            SpawnManager.Instance.DespawnBags(paperBag);

                        AreaManager.Instance.SpawnArea.DespawnCustomers(this);
                    }
                }
            }
        }
        #endregion
    }

    public void Move(Vector3 dir)
    {
        isTalking = false;
        nmAgent.isStopped = false;
        nmAgent.ResetPath();
        nmAgent.SetDestination(dir);
        targetPos = dir;
        print(dir);
    }

    public void PutInBag(Vector3 dir)
    {
        if (breadList.Count > 0)
        {
            Bread bread = breadList[breadList.Count - 1];
            breadList.Remove(bread);

            bread.transform.
                DOJump(dir, 2f, 1, 0.3f).OnComplete(() =>
                {
                    SpawnManager.Instance.DespawnBreads(bread);
                });
        }
    }
    public void GetBag(PaperBag bag)
    {
        paperBag = bag;
    }

    public void TaskOver(Area area)
    {
        nextArea = area; // 다음 지역 준비
        complete = false;
        isWaiting = false;
        #region 이모지, commentBox관리
        if (nextArea != null)
        {
            if (nextArea == AreaManager.Instance.basketArea.GetComponent<Area>())
            {
                state = ECustomer.Bread;
                uiState.ShowCommentBox("B", needAmount);
            }
            else if (nextArea == AreaManager.Instance.posArea.GetComponent<Area>())
            {
                state = ECustomer.Pay;
                if(isTakeOut) 
                    uiState.ShowCommentBox("P", 0);
                else
                    uiState.ShowCommentBox("C", 0);
            }
            else if (nextArea == AreaManager.Instance.SpawnArea.GetComponent<Area>())
            {
                state = ECustomer.Home;
                happyEmoji.gameObject.SetActive(true);
                happyEmoji.Play();
                uiState.ShowCommentBox("asd", 0);
            }
            else if (nextArea == AreaManager.Instance.cafeArea.GetComponent<Area>())
            {
                state = ECustomer.Cafe;
                uiState.ShowCommentBox("C", 0);
            }
        }
        #endregion

        Move(nextArea.transform.position);
    }

    public void GetBread(List<Bread> crois)
    {
        if (breadList.Count >= maxValue ||
            breadList.Count == needAmount ||
            complete)
            return;

        isWaiting = true;

        int takeAmount = 0;
        int nAmount = needAmount - breadList.Count;

        if (breadList.Count != 0)
            takeAmount = crois.Count - nAmount <= 0 ? crois.Count : nAmount;
        else
        {
            if (crois.Count < needAmount)
                takeAmount = crois.Count;
            else
                takeAmount = needAmount;
        }

        StartCoroutine(GetRoutine(takeAmount, crois));
    }

    IEnumerator GetRoutine(int takeAmount, List<Bread> crois)
    {
        isStacking = true;
        anim.SetBool(stack, isStacking ? true : false);

        CalcPosition calc = new CalcPosition(needAmount, breadPos.position, y);
        List<Vector3> vecList = calc.SetBreadPos(breadList.Count, takeAmount);

        int i = breadList.Count;
        int k = 0;
        for (int j = i; j < takeAmount + i; j++)
        {
            Bread bread = crois[crois.Count - 1];

            crois.Remove(bread);
            breadList.Add(bread);
            if (vecList.Count > 0)
            {
                print(vecList.Count);
            }
            bread.GetFromOven(vecList[k], breadPos);
            ++k;
            yield return new WaitForSeconds(0.3f);
            areaCoolDown = 0;
        }

        if (needAmount == breadList.Count)
        {
            complete = true;
            isWaiting = false;
        }
        else
            complete = false;

        isWaiting = true;
        yield return null;
    }

    public Bread PutBread(Transform parent)
    {
        Bread bread = breadList[0];
        breadList.RemoveAt(0);
        bread.transform.parent = parent;

        return bread;
    }
    public void ChangeTakeOut()
    {
        isTakeOut = true;
    }
    private void ChoiceReset()
    {
        happyEmoji.Stop();
        happyEmoji.gameObject.SetActive(false);

        isStacking = false;
        complete = false;
        isWaiting = false;
        anim.SetBool(stack, isStacking ? true : false);

        needAmount = Random.Range(1, maxValue);
        isTakeOut = needAmount == 1 ? false : true;

        if(AreaManager.Instance.cafeArea.isFilled)
            isTakeOut = true;

        nextArea = AreaManager.Instance.basketArea;

    }

    public bool CanGo()
    {
        return complete;
    }
}
