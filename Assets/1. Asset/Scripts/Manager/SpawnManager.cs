using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public Transform customerSpawn;

    public List<Bread> breadPoolList;
    public List<Customer> customersPoolList;
    public List<Bill> billsPoolList;

    public Bread breadPrefab;
    public Customer customerPrefab;
    public Bill billPrefab;

    public int breadInteger;
    public int customerInteger;
    public int billInteger;

    #region Singleton

    public static SpawnManager Instance 
    {
        get {
            if (instance == null)
                return null;

            return instance;
        }
    }
    
    private static SpawnManager instance;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            DestroyImmediate(this.gameObject);
    }
    #endregion

    void Start()
    {
        CreateObject();
    }


    #region Spawn Object
    public Bill SpawnBill(Transform parent, Vector3 spawnPos)
    {
        if(billsPoolList.Count <= 0) return null;

        Bill bil = billsPoolList[0];
        billsPoolList.RemoveAt(0);
        bil.transform.parent = parent;
        bil.transform.position = spawnPos;
        return bil;
    }

    public Customer SpawnCustomers()
    {
        if(customersPoolList.Count <= 0) return null;

        Customer cus = customersPoolList[0];
        customersPoolList.RemoveAt(0);
        cus.transform.parent = customerSpawn;
        cus.transform.position = customerSpawn.position;
        return cus;
    }
    public Bread SpawnCroissants(Transform parent,Vector3 spawnPos)
    {
        if (breadPoolList.Count <= 0) return null;

        Bread crois = breadPoolList[0];
        breadPoolList.RemoveAt(0);
        crois.transform.parent = parent;
        crois.transform.position = spawnPos;
        return crois;
    }
    #endregion

    #region Despawn Object
    public void DespawnBills(Bill bil)
    {
        if (billsPoolList.Count >= billInteger)
            Destroy(bil.gameObject);

        else
        {
            billsPoolList.Insert(0, bil);
            bil.gameObject.transform.SetParent(this.transform);
            bil.gameObject.SetActive(false);
        }
    }

    public void DespawnCustomers(Customer cus)
    {
        if (customersPoolList.Count >= customerInteger)
            Destroy(cus.gameObject);

        else
        {
            customersPoolList.Insert(0, cus);
            cus.gameObject.transform.SetParent(this.transform);
            cus.gameObject.SetActive(false);
        }
    }
    public void DespawnCroissants(Bread croi)
    {
        if (breadPoolList.Count >= breadInteger)
            Destroy(croi);

        else
        {
            breadPoolList.Insert(0, croi);
            croi.transform.SetParent(this.transform);
            croi.gameObject.SetActive(false);
        }
    }
    #endregion

    private void CreateObject()
    {
        if(breadPoolList.Count < breadInteger)
        {
            int j = breadPoolList.Count;

            for (int i = 0; i < breadInteger - j; i++)
            {
                Bread croi = Instantiate(breadPrefab);
                breadPoolList.Insert(0, croi);
                croi.gameObject.SetActive(false);
                croi.transform.parent = this.transform;
            }
        }

        if(customersPoolList.Count < customerInteger)
        {
            int j = customersPoolList.Count;

            for (int i = 0; i < customerInteger - j; i++)
            {
                Customer cus = Instantiate(customerPrefab);
                customersPoolList.Insert(0, cus);
                cus.gameObject.SetActive(false);
                cus.transform.parent = this.transform;
            }
        }
        if (billsPoolList.Count < billInteger)
        {
            int j = billsPoolList.Count;

            for (int i = 0; i < billInteger - j; i++)
            {
                Bill bil = Instantiate(billPrefab);
                billsPoolList.Insert(0, bil);
                bil.gameObject.SetActive(false);
                bil.transform.parent = this.transform;
            }
        }
    }

    
    void Update()
    {
        
    }
}
