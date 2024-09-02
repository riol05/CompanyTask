using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour // 디버그용
{

    #region Singleton
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }
    #endregion

    [HideInInspector]
    public Vector2 move;
    [HideInInspector]
    public bool isMove;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            DestroyImmediate(this.gameObject);
    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    private void MoveInput(Vector2 dir)
    {
        dir.y = dir.y == 0 ? 0 : (dir.y < 0 ? -1 : 1);
        dir.x = dir.x == 0 ? 0 : (dir.x < 0 ? -1 : 1);
        move = dir;
    }
}
