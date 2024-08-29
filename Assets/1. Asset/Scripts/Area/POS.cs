using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POS : Area
{
    public Transform laneForPay; // 계산줄
    public Transform laneForCafe; // 카페줄

    public Area nextAreaIsCafe; // 다음 행선지 카페// 나머지 한곳은 Exit

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
