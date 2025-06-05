using System;
using UnityEngine;

public class CameraSpawning : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private MapScriptableObject mapData;
    void Awake()
    {
        if(mapData.MapSize == new Vector2(20, 20))
        {
            mainCamera.transform.position = spawnPoints[0].position;
            Debug.Log("Spawning camera at 20x20 map size");
        }
        else if (mapData.MapSize == new Vector2(30, 30))
        {
            mainCamera.transform.position = spawnPoints[1].position;
            Debug.Log("Spawning camera at 30x30 map size");
        }
        else if(mapData.MapSize == new Vector2(40, 40))
        {
            Debug.Log("Spawning camera at 40x40 map size");
            mainCamera.transform.position = spawnPoints[2].position;
        }
        else if(mapData.MapSize == new Vector2(50, 50))
        {
            mainCamera.transform.position = spawnPoints[3].position;
            Debug.Log("Spawning camera at 50x50 map size");
        }
    }
}
