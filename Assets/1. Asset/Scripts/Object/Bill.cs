using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Bill : MonoBehaviour
{
    public int biilsOnBoard;
    public bool canJump = false;

    public void GetFromGround(Vector3 dir)
    {
        if (canJump)
        {
            transform.DOJump(dir, 1f, 1, 0.2f).OnComplete(() =>
                {
                    canJump = false;
                    SpawnManager.Instance.DespawnBills(this);
                });
        }
    }
}