using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Oven : Area
{
    public Bread breadPrefab;
    public Transform breadSpawnPos;
    public Transform ovenEdge;
    public Transform Basket;

    List<Bread> breadList;
    private float breadTime;


    public override void Start()
    {
        base.Start();
        breadList = new List<Bread>();
    }
    private void FixedUpdate()
    {
        breadTime += Time.deltaTime;
        if(breadTime > 5)
        {
            breadTime = 0;
            InteractArea();
        }
    }

    public override void InteractArea()
    {
        if (curAmount == maxValue)
        {
            curAmount = maxValue;
            return;
        }
        Bread crois = EntranceCroissant();
        breadList.Add(crois);
        crois.transform.DOMove(ovenEdge.position, 0.5f);
    }

    public override void ArrowActive(bool ison)
    {
        base.ArrowActive(ison);
    }

    public void ManagedByPlayer(Pocket pocket)
    {
        base.ManagedByPlayer();

        pocket.InActiveBread(curAmount);
        

    }


    private Bread EntranceCroissant()
    {
        return SpawnManager.Instance.SpawnCroissants(breadSpawnPos, breadSpawnPos.position);
    }


}
