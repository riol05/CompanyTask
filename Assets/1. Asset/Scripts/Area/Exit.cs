using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Area
{
    public override void CheckAreaIsFull()
    {
    }

    public override void InteractOnArea()
    {
        // 만약 Customer 상태가 Home일때 상태 바꿔주고 Despawn
    }

    public override void ArrowActive()
    {
    }
    public override void MoveNextArea()
    {
    }
}
