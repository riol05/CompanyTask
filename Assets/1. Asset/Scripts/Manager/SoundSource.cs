using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.AddAllSounds(gameObject);
    }

    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
