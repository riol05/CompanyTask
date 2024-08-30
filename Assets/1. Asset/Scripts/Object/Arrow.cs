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

    public Transform trackingArrow; // 캐릭터 아래에 있는 AI
    public EArrowDir state;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

}
