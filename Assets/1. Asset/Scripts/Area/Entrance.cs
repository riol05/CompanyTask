using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : Area
{
    new void Start()
    {

    }

    public override void InteractArea()
    {
        // 만약 Customer 상태가 Home일때 상태 바꿔주고 Despawn
    }
    public override void MoveNextArea()
    {
    }
}
