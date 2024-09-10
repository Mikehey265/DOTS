using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate;
    private float spawnTime;

    private void Update()
    {
        if (spawnTime < Time.time)
        {
            Instantiate(prefab);
            spawnTime = Time.time + spawnRate;   
        }
    }
}
