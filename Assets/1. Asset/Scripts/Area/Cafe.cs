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

    void Start()
    {
        state = ETableState.Normal;
    }

    public override void CheckAreaIsFull()
    {
    }

    public override void InteractOnArea()
    {
    }

    public override void ArrowActive()
    {
    }
    public override void MoveNextArea()
    {
    }
}
