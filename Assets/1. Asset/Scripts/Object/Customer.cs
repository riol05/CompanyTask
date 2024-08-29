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
    Cal,
    Home
}
public class Customer : MonoBehaviour
{
    public float speed;
    public NavMeshAgent nmAgent;
    public Animator animator;
    public GameObject uiState;

    private Area nextArea;

    ECustomer state;

    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        state = ECustomer.Bread;
    }

    void Update()
    {
    }

    private void Move()
    {
        nmAgent.SetDestination(nextArea.transform.position);
    }
    private void WaitForPlayer()
    {

    }
}
