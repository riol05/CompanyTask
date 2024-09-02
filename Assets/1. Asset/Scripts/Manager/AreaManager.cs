using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    #region Singleton

    public static AreaManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    private static AreaManager instance;
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
    public Cafe cafeArea;
    public Spawn SpawnArea;
    public Basket basketArea;
    public Oven ovenArea;
    public POS posArea;
    public BillArea moneyArea;
}
