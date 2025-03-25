using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject BoxPrefabs;

    int boxCount = 10;
    float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBoxWave();
    }

    void HanderBoxDestroyed()
    {
        boxCount++;
        Debug.Log($"Box Destroyed, boxCount: {boxCount}");
    }

    void SpawnBoxWave()
    {
        currentTime += Time.deltaTime;

        if(boxCount > 0 && currentTime > 3)
        {
            SpawnBox();
            boxCount--;
            currentTime = 0;
        }
    }

    void SpawnBox()
    {
        Vector3 spawnPos = GetRandomSpawnPosition(5f);
        //Instantiate(BoxPrefabs, spawnPos, Quaternion.identity);
        SpawnerManager.Instance.SpawnObject(BoxPrefabs, spawnPos, Quaternion.identity, HanderBoxDestroyed);
    }

    Vector3 GetRandomSpawnPosition(float offset)
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2;
        float camWidth = camHeight * cam.aspect;

        Vector3 camPosition = cam.transform.position;

        float left = camPosition.x - (camWidth / 2);
        float right = camPosition.x + (camWidth / 2);
        float top = camPosition.y + (camHeight / 2);
        float bottom = camPosition.y - (camHeight / 2);

        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0:
                spawnPos = new Vector3(Random.Range(left - offset, left), Random.Range(bottom, top), 0);
                break;
            case 1:
                spawnPos = new Vector3(Random.Range(right + offset, right), Random.Range(bottom, top), 0);
                break;
            case 2:
                spawnPos = new Vector3(Random.Range(left, right), Random.Range(top + offset, top), 0);
                break;
            case 3:
                spawnPos = new Vector3(Random.Range(left, right), Random.Range(bottom - offset, bottom), 0);
                break;
        }

        return spawnPos;
    }

}
