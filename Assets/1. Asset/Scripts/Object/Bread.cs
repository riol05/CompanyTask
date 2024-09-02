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

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
         rb.isKinematic = false;
    }
    private void PutInBag()
    {

    }

    public void GetFromOven(Vector3 dir, Transform parent)
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        this.transform.parent = parent;
        
        Vector3 targetPos = new Vector3(parent.position.x, dir.y, parent.position.z);

        transform.DOJump(targetPos, 2f, 1, 0.2f).OnComplete(() =>
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            this.transform.localRotation = targetRotation;
            transform.localPosition = new Vector3(0,dir.y,0);
            SoundManager.instance.PlaySound("GetObject");
        });
    }
    public void PutOnPosition(Vector3 dir, Transform parent)
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        this.transform.parent = parent;

        transform.DOJump(dir, 2f, 1, 0.2f).OnComplete(() =>
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            this.transform.localRotation = targetRotation;
            SoundManager.instance.PlaySound("PutObject");
        });
    }
}