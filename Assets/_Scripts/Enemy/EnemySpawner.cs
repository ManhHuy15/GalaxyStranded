using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveSO[] WaveSO;
    public List<GameObject> EnemyPrefabs;

    private int curentWave = 0;
    private float currentTime;

    void Start()
    {
        StartCoroutine(NextWaveRotine());
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        SpawnWave();
    }

    void SpawnRandom()
    {
        float posX = Random.Range(-9f, 9f);
        int randomEnemy = Random.Range(0, EnemyPrefabs.Count);
        SpawnerManager.Instance.SpawnObject(EnemyPrefabs[randomEnemy], new Vector3(posX, 5, -2), Quaternion.identity);
    }

    private IEnumerator NextWaveRotine()
    {
        yield return new WaitForSeconds(WaveSO[curentWave].timeBetweenWave);
        NextWave();
    }

    void NextWave()
    {
        curentWave++;
        Debug.Log("Wave " + curentWave);
    }

    void SpawnWave()
    {
        if (curentWave >= WaveSO.Length) return;

        if (currentTime >= WaveSO[curentWave].spawnDelay)
        {
            SpawnRandom();
            currentTime = 0;
        }
    }
}
