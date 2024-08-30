using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POS : Area
{
    public Transform laneForPay; // 계산줄
    public Transform laneForCafe; // 카페줄
    public BillArea billArea;

    public Area nextAreaIsCafe; // 다음 행선지 카페// 나머지 한곳은 Exit

    public int breadCost;
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
        billArea.posArea.InteractArea(breadCost);
    }
    public override void WaitForPlayer()
    {
        base.WaitForPlayer();   
    }
    public override void MoveNextArea()
    {
    }
    public override void ManagedByPlayer()
    {
        base.ManagedByPlayer();
    }
}
