using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public FloatingJoystick joystick;
    public float moveSpeed;
    public float roatateSpeed;
    private float speed;
    bool isStacking = false;

    float x;
    float y;
    Vector2 dir;
    private Rigidbody rb;
    [SerializeField]
    private Animator anim;

    #region AnimatorString
    private string stack;
    private string Idle;
    private string Run;

    private void AnimString()
    {
        stack = "Stack";
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
        AnimString(); // 애니메이션 관련 함수
    }

    private void FixedUpdate()
    {
        dir.x = joystick.Horizontal;
        dir.y = joystick.Vertical;

        rb.angularVelocity = Vector3.zero;
        Move(dir);
    }
    private void Move(Vector2 moveDir)
    {
        Vector3 inputDir = new Vector3(-moveDir.x, 0, -moveDir.y);
        
        speed = moveDir != Vector2.zero ? 1 : 0;

        if (speed > 0)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(Run))
            {
                print("123");
                anim.SetTrigger(Run);
            }
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(Idle))
            {
                print("123");
                anim.SetTrigger(Idle);
            }
        }

        anim.SetBool(stack, isStacking);


        if (moveDir == Vector2.zero)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        RotateDirection(inputDir);
        inputDir *= moveSpeed;
        rb.velocity = inputDir;
    }
    private void RotateDirection(Vector3 lookDir) 
    {
        Quaternion rot = Quaternion.LookRotation(lookDir);
        rb.rotation = Quaternion.Slerp(rb.rotation, rot, roatateSpeed * Time.deltaTime);
    }

    public void ChangeStackingState(bool isOn)
    {
        isStacking = isOn;
    }
}