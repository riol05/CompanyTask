using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float roatateSpeed;

    bool isStacking = false;
    
    private Rigidbody rb;
    [SerializeField]
    private Animator anim;

    #region AnimatorString
    private string stackIdle;
    private string stackRun;
    private string Idle;
    private string Run;

    private void AnimString()
    {
        stackIdle = "stackIdle";
        stackRun = "stackRun";
        Idle = "Idle";
        Run = "Run";
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        AnimString();
    }

    private void FixedUpdate()
    {
        Move(InputManager.Instance.move);
    }
    private void Move(Vector2 moveDir)
    {
        Vector3 inputDir = new Vector3(moveDir.x, 0, moveDir.y);
        if (InputManager.Instance.move == Vector2.zero)
        {
            rb.velocity = Vector3.zero;

            if (isStacking)
                anim.SetTrigger(stackIdle);
            else
                anim.SetTrigger(Idle);
            return;
        }
        else
        {
            RotateDirection(inputDir);
        }

        if (isStacking)
            anim.SetTrigger(stackRun);
        else
            anim.SetTrigger(Run);

        inputDir *= moveSpeed;
        rb.velocity = inputDir;
    }
    private void RotateDirection(Vector3 lookDir) 
    {
        
        Quaternion rot = Quaternion.LookRotation(lookDir);
        rb.rotation = Quaternion.Slerp(rb.rotation, rot, roatateSpeed * Time.deltaTime);
    }

}