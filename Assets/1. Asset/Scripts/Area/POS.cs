using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POS : Area
{
    public Transform laneForPay; // �����
    public Transform laneForCafe; // ī����

    public Area nextAreaIsCafe; // ���� �༱�� ī��// ������ �Ѱ��� Exit

    public override void ArrowActive()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckAreaIsFull()
    {
        throw new System.NotImplementedException();
    }

    public override void InteractOnArea()
    {
        throw new System.NotImplementedException();
    }
    public override void MoveNextArea()
    {
    }
}
