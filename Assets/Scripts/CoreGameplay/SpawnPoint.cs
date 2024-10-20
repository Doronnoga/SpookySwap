using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
   private Vector2 spawnPoint;
   private Vector2 startPoint;
    private Vector2 latestCheckPoint;

    // Update is called once per frame
    void Start()
    {
        startPoint = transform.position;
        spawnPoint = startPoint;
    }
}
