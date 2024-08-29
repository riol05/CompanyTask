using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taking : Area
{
    public Transform spawnPos;
    public Vector3 nextSpawnPos;
    public override void ArrowActive()
    {
    }

    public override void CheckAreaIsFull()
    {
    }

    public override void InteractOnArea()
    {
    }
    public override void MoveNextArea()
    {
    }

    private void SetMoneyOnBoard(int i, Taking area)
    {

        for (int j = 0; j < i; j++)
        {
            SpawnManager.Instance.SpawnBill(transform, nextSpawnPos); // 
        }
    }
}
