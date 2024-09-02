using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawn : Area
{
    public Transform spawnParent;

    RaycastHit hit;
    
    private int randomCount = 1;
    private float spawnCoolDown;
    private float despawnCoolDown;

    private List<Customer> customerList;
    private int customerCount;

    private void Awake()
    {
        customerList = new List<Customer>();
    }

    private void FixedUpdate()
    {
        spawnCoolDown += Time.deltaTime;
        despawnCoolDown += Time.deltaTime;

        if (customerCount != maxValue)
        {
            if (spawnCoolDown > randomCount)
            {
                spawnCoolDown = 0;
                randomCount = Random.Range(2, 4) * 2;
                InteractArea();
            }
        }
    }

    public override void InteractArea()
    {
        Customer cust =
            SpawnManager.Instance.SpawnCustomers(spawnParent.position,spawnParent);
        ++customerCount;

        cust.TaskOver(nextArea);
    }
    private void MoveNextArea(Area area)
    {
    }
    public override void ManagedByPlayer()
    {

    }

    public void DespawnCustomers(Customer cust)
    {
        SpawnManager.Instance.DespawnCustomers(cust);
        --customerCount;

    }
}
