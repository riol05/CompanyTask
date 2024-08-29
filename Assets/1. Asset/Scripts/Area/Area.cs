using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour, IArea
{
    public float areaRange;
    public int areaCount;
    public bool isFull;
    public Arrow arrow;

    public Area nextArea;

    public virtual void ArrowActive()
    {
        throw new System.NotImplementedException();
    }

    public virtual void CheckAreaIsFull()
    {
        throw new System.NotImplementedException();
    }

    public virtual void InteractOnArea()
    {
    }

    public virtual void MoveNextArea()
    {
        throw new System.NotImplementedException();
    }
}
