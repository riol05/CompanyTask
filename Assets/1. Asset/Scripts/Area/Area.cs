using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour, IArea
{
    public bool isFilled; // Æ©Åä¸®¾ó ¿ë

    public float areaRange;
    public int maxValue;
    public bool isFull;
    public Arrow arrow;
    public int curAmount;
    
    public Area nextArea; // List?? Array??
    public virtual void Start()
    {
        isFilled = false;
        isFull = false;
        ManagedByPlayer();
    }
    public virtual void ArrowActive(bool isOn)
    {
        arrow.gameObject.SetActive(isOn);
    }
    public virtual void InteractArea()
    {
    }

    public virtual void ManagedByPlayer()
    {
        isFilled = false;
        ArrowActive(isFilled);
    }

    public virtual void MoveNextArea()
    {
    }

    public virtual void WaitForPlayer()
    {
    }


    public IEnumerator GetObjectAnimation()
    {
        yield return null;
    }
}
