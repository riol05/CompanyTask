using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bread : MonoBehaviour
{
    // Y : 0.4 X3까지 가능 Customer
    // Y : 0.4 X5까지 Player
    // X : 0.5 X2  Z: 1.1 X 5 가능 Basket
    public int breadInPocket;
    public float moveDistance;

    void Update()
    {
        
    }


    private void PutInBag()
    {

    }

    public void GetFromOven(Vector3 dir)
    {
        transform.DOJump(dir, 1f, 1, 0.2f);
        //    .OnComplete(() =>
        //{
        //    SpawnManager.Instance.DespawnBreads(this);
        //});
    }

}
