using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeEditor;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public Transform customerSpawn;

    public List<Bread> breadPoolList;
    public List<Customer> customersPoolList;
    public List<Bill> billsPoolList;
    public List<PaperBag> paperBagPoolList;

    public Bread breadPrefab;
    public Customer customerPrefab;
    public Bill billPrefab;
    public PaperBag bagPrefab;

    public int breadMaxValue;
    public int customerMaxValue;
    public int billMaxValue;
    public int bagMaxValue;

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
        if(billsPoolList.Count == 0)
        {
            CreateObject();
        }
        Bill bil = billsPoolList[0];
        bil.gameObject.SetActive(true);
        billsPoolList.RemoveAt(0);

        bil.transform.parent = parent;
        bil.transform.position = spawnPos;
        bil.canJump = true;

        return bil;
    }

    public Customer SpawnCustomers(Vector3 dir, Transform parent)
    {
        if(customersPoolList.Count == 0) return null;

        Customer cus = customersPoolList[0];
        cus.gameObject.SetActive(true);
        customersPoolList.RemoveAt(0);

        cus.transform.parent = parent;
        cus.transform.position = dir;

        return cus;
    }

    public Bread SpawnCroissants(Transform parent,Vector3 spawnPos)
    {
        if (breadPoolList.Count == 0) return null;

        Bread crois = breadPoolList[0];
        crois.gameObject.SetActive(true);
        breadPoolList.RemoveAt(0);

        crois.transform.parent = parent;
        crois.transform.position = spawnPos;

        return crois;
    }

    public PaperBag Spawnpaperbag(Transform parent, Vector3 spawnPos)
    {
        if (paperBagPoolList.Count == 0) return null;

        PaperBag bag = paperBagPoolList[0];
        bag.gameObject.SetActive(true);
        paperBagPoolList.RemoveAt(0);

        bag.transform.parent = parent;
        bag.transform.position = spawnPos;

        return bag;
    }
    #endregion

    #region Despawn Object
    public void DespawnBills(Bill bil)
    {
        if (billsPoolList.Count > billMaxValue)
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
        if (customersPoolList.Count > customerMaxValue)
            Destroy(cus.gameObject);

        else
        {
            customersPoolList.Insert(0, cus);
            cus.gameObject.transform.SetParent(this.transform);
            cus.gameObject.SetActive(false);
        }
    }

    public void DespawnBreads(Bread croi)
    {
        if (breadPoolList.Count > breadMaxValue)
            Destroy(croi);

        else
        {
            breadPoolList.Insert(0, croi);
            croi.transform.SetParent(this.transform);
            croi.gameObject.SetActive(false);
        }
    }
    public void DespawnBags(PaperBag bag)
    {
        if (paperBagPoolList.Count > bagMaxValue)
            Destroy(bag.gameObject);

        else
        {
            paperBagPoolList.Insert(0, bag);
            bag.gameObject.SetActive(false);
            bag.gameObject.transform.parent = this.transform;
        }
    }
    #endregion

    private void CreateObject()
    {
        if(breadPoolList.Count < breadMaxValue)
        {
            int j = breadPoolList.Count;

            for (int i = 0; i < breadMaxValue - j; i++)
            {
                Bread croi = Instantiate(breadPrefab);
                breadPoolList.Insert(0, croi);
                croi.gameObject.SetActive(false);
                croi.transform.parent = this.transform;
            }
        }

        if(customersPoolList.Count < customerMaxValue)
        {
            int j = customersPoolList.Count;

            for (int i = 0; i < customerMaxValue - j; i++)
            {
                Customer cus = Instantiate(customerPrefab);
                customersPoolList.Insert(0, cus);
                cus.gameObject.SetActive(false);
                cus.transform.parent = this.transform;
            }
        }

        if (billsPoolList.Count < billMaxValue)
        {
            int j = billsPoolList.Count;

            for (int i = 0; i < billMaxValue - j; i++)
            {
                Bill bil = Instantiate(billPrefab);
                billsPoolList.Insert(0, bil);
                bil.gameObject.SetActive(false);
                bil.transform.parent = this.transform;
            }
        }

        if(paperBagPoolList.Count < bagMaxValue)
        {
            int j = paperBagPoolList.Count;

            for (int i = 0; i < bagMaxValue - j; i++)
            {
                PaperBag bag = Instantiate(bagPrefab);
                paperBagPoolList.Insert(0, bag);
                bag.gameObject.SetActive(false);
                bag.transform.parent = this.transform;
            }
        }
    }
}
