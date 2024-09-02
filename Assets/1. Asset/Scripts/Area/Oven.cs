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
    float handCoolDown;

    List<Bread> breadList;
    private float breadTime;

    RaycastHit hitPlayer;
    public override void Start()
    {
        base.Start();
        breadList = new List<Bread>();
    }
    private void FixedUpdate()
    {
        curAmount = breadList.Count;
        breadTime += Time.deltaTime;
        if(breadTime > 5)
        {
            breadTime = 0;
            InteractArea();
        }
    }

    public override void InteractArea()
    {
        if (curAmount >= maxValue) 
        {
            curAmount = maxValue;
            if(breadList.Count != curAmount)
            {
                while (breadList.Count != curAmount)
                {
                    SpawnManager.Instance.DespawnBreads(breadList[breadList.Count - 1]);
                    breadList.RemoveAt(breadList.Count - 1);
                }
            }
            return;
        }// maxValue 벗어나는 상황 방지 코드
        
        Bread crois = EntranceCroissant();
        if(crois == null)
            return;

        crois.transform.DOMove(ovenEdge.position, 0.5f).OnComplete(() =>
            {
                breadList.Add(crois);
                isFilled = true;
            });
    }

    public override void ArrowActive(bool ison)
    {
        base.ArrowActive(ison);
    }

    public void ManagedByPlayer(Pocket pocket)
    {
        if (!isFilled) return;
        isFilled = false;
        pocket.StackingOnHand(curAmount,breadList,isFilled);
        base.ManagedByPlayer();
    }


    private Bread EntranceCroissant()
    {
        return SpawnManager.Instance.SpawnCroissants(breadSpawnPos, breadSpawnPos.position);
    }


}
