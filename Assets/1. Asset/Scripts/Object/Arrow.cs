using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TreeEditor;

public enum EArrowDir
{

}
public class Arrow : MonoBehaviour
{
    private Animator anim;

    public Transform trackingArrow; // ĳ���� �Ʒ��� �ִ� AI
    public EArrowDir state;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

}
