using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    // Y : 0.4 X3까지 가능 Customer
    // Y : 0.4 X5까지 Player
    // X : 0.5 X2  Z: 1.1 X 5 가능 Basket
    public int breadInPocket;

    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();   
    }
    void Start()
    {
        GetFromOven();   
    }

    void Update()
    {
        
    }

    private void PutInBag()
    {

    }

    public void GetFromOven()
    {
        Vector3 moveDir = new Vector3(0,0,1);
        moveDir *= 1;
        rb.velocity = moveDir;
    }

    
}
