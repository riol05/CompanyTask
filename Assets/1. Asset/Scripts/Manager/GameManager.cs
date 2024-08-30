using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    private static GameManager instance;
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

    public LayerMask player;
    public LayerMask customer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
