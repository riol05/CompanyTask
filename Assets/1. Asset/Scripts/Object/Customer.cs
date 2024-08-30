using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum ECustomer
{
    None,
    Bread,
    Table,
    Calc,
    Home
}
public class Customer : MonoBehaviour
{
    public float speed;
    public GameObject uiState;

    private LayerMask areaLayer;
    private Area nextArea;
    private NavMeshAgent nmAgent;
    private Animator anim;

    public ECustomer state;

    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        state = ECustomer.Bread;
    }

    void Update()
    {
        
    }

    public void Move(Vector3 dir) => nmAgent.SetDestination(dir);

    private void WaitForTask(Vector3 dir)
    {
        Move(dir);

    }

    private void TaskOver(Area area)
    {
        nextArea.InteractArea(); // 마무리하고 
        
        nextArea = area; // 다음 지역 준비
        Move(nextArea.transform.position);

        if (!nextArea.isFilled)
        {
            WaitForTask(nextArea.transform.position);
        }
    }
    

    private ECustomer ChangeState(ECustomer custState)
    {

        return state;
    }
}
