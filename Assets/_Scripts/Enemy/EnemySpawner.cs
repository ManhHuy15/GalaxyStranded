using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected WaveSO[] WaveSO;
    [SerializeField] protected List<GameObject> EnemyPrefabs;
    [SerializeField] protected List<Transform> points;

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
        Transform pos = GetRandomPoint();
        int randomEnemy = Random.Range(0, EnemyPrefabs.Count);
        SpawnerManager.Instance.SpawnObject(EnemyPrefabs[randomEnemy], pos.position, Quaternion.identity);
    }

    private Transform GetRandomPoint()
    {
        int rand = Random.Range(0, this.points.Count);
        return this.points[rand];
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
