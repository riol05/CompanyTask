using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Area
{

    private void Start()
    {
        EntranceCroissant();
    }
    public Transform breadSpawnPos;

    public override void CheckAreaIsFull()
    {
        throw new System.NotImplementedException();
    }

    public override void InteractOnArea()
    {
        throw new System.NotImplementedException();
    }

    public override void ArrowActive()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveNextArea()
    {
    }


    private void EntranceCroissant()
    {
        SpawnManager.Instance.SpawnCroissants(transform, breadSpawnPos.position);
    }
}
