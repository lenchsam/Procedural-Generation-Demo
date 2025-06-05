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
        }
        else if (mapData.MapSize == new Vector2(30, 30))
        {
            mainCamera.transform.position = spawnPoints[1].position;
        }
        else if(mapData.MapSize == new Vector2(40, 40))
        {
            mainCamera.transform.position = spawnPoints[2].position;
        }
        else if(mapData.MapSize == new Vector2(50, 50))
        {
            mainCamera.transform.position = spawnPoints[3].position;
        }
    }
    public void newScene(string sceneName)
    {
        GameObject sceneManager = GameObject.Find("----SceneManager----");
        sceneManager.GetComponent<SceneManager>().LoadScene(sceneName);
    }
}
