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
    private Dictionary<EAnimationStatus, string> animationString = new Dictionary<EAnimationStatus, string>();

    enum EAnimationStatus
    {
        RUN,
        IDLE,
        STACK
    }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        animationString.Add(EAnimationStatus.RUN, "Run");
        animationString.Add(EAnimationStatus.IDLE, "Idle");
        animationString.Add(EAnimationStatus.STACK, "Stack");
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
        if(speed > 0)
        {
            anim.SetBool(animationString[EAnimationStatus.RUN],true);
        }
        else
        {
            anim.SetBool(animationString[EAnimationStatus.RUN], false);
        }

        //if (speed > 0)
        //{
        //    EAnimationStatus nowStatus = EAnimationStatus.RUN;

        //    if (anim.GetBool(animationString[nowStatus]) == false)
        //    {
        //        anim.SetBool(animationString[nowStatus],true);
        //        anim.SetBool(animationString[EAnimationStatus.IDLE], false);
        //        Debug.Log("RUN!");
        //    }
        //}
        //else
        //{
        //     EAnimationStatus nowStatus = EAnimationStatus.IDLE;

        //     if (anim.GetBool(animationString[nowStatus]) == false)
        //     {
        //        anim.SetBool(animationString[nowStatus], true);
        //        anim.SetBool(animationString[EAnimationStatus.RUN], false);
        //        Debug.Log("IDLE!");
        //     }
        //}

        anim.SetBool(animationString[EAnimationStatus.STACK], isStacking);

        Debug.Log($"run : {anim.GetBool(animationString[EAnimationStatus.RUN])}," +
            $"stack : {anim.GetBool(animationString[EAnimationStatus.STACK])}");



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