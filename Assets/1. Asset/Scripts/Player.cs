using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Pocket pocket;


    private void Awake()
    {
        pocket = GetComponent<Pocket>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }


}
