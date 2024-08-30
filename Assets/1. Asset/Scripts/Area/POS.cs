using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POS : Area
{
    public Transform laneForPay; // �����
    public Transform laneForCafe; // ī����
    public BillArea billArea;

    public Area nextAreaIsCafe; // ���� �༱�� ī��// ������ �Ѱ��� Exit

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
