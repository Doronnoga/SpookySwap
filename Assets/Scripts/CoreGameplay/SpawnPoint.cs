using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Vector2 spawnPoint;
    private Vector2 startPoint;
    private Vector2 latestCheckPoint;

    private string tagToCompare = "CheckPoint";
    private string tagToSendBack = "Hurt";

    void Start()
    {
        startPoint = transform.position;
        spawnPoint = startPoint; // Initialize spawnPoint to the start position
        Debug.Log($"Initial spawn point set to: {spawnPoint}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag(tagToCompare))
            {
                Debug.Log("Checkpoint reached");
                latestCheckPoint = collision.transform.position;
                spawnPoint = latestCheckPoint; // Set new checkpoint
                Debug.Log($"Checkpoint set to: {spawnPoint}");
            }

            if (collision.CompareTag(tagToSendBack))
            {
                Debug.Log("Returning to spawn point");
                transform.position = spawnPoint; // Reset player position to last checkpoint
            }
        }
    }
}
