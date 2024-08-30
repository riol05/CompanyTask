using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : Area
{
    public override void Start()
    {
        base.Start();
    }
    public override void ArrowActive(bool ison)
    {
        base.ArrowActive(ison);
    }

    public override void InteractArea()
    {
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
