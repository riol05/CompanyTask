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
    
    public Transform chair;
    public Transform trash;

    public ParticleSystem cleanParticle;
    public ParticleSystem openParticle;
    
    private bool isOpen = false;

    public override void Start()
    {
        base.Start();
        state = ETableState.Normal;
    }
    public void WorkingNow(bool isOn)
    {
        inActiveArea.gameObject.SetActive(isOn);
    }


    public override void InteractArea()
    {
        base.InteractArea();
    }

    public override void ArrowActive(bool isOn)
    {
        base.ArrowActive(isOn);
    }
    public override void MoveNextArea()
    {
    }
    public override void ManagedByPlayer()
    {
        base.ManagedByPlayer();
    }
    public override void WaitForPlayer()
    {
        base.WaitForPlayer();
    }
}
