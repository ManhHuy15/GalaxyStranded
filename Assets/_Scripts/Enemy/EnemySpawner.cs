using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected WaveSO[] WaveSO;
    [SerializeField] protected List<GameObject> EnemyPrefabs;
    [SerializeField] protected List<Transform> points;
    [SerializeField] protected GameObject waveUI;
    [SerializeField] protected Text waveText;

    private int curentWave = 0;
    private float currentTime;
    private bool stopSpawn = false;
    [SerializeField] private int countEnemy = 0;

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
        countEnemy++;
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
        if (curentWave < WaveSO.Length)
        {
            AnimationWaveChange();
        }
    }

    void AnimationWaveChange()
    {
        Animator animator = waveUI.GetComponent<Animator>();
        waveText.text = WaveSO[curentWave].waveName;
        animator.SetBool("isNextWave", true);
        stopSpawn = true;
        StartCoroutine(AnimationWaveChangeRotine());
    }

    private IEnumerator AnimationWaveChangeRotine()
    {
        yield return new WaitForSeconds(5f);
        waveUI.GetComponent<Animator>().SetBool("isNextWave", false);
        stopSpawn = false;
        StartCoroutine(NextWaveRotine());
    }

    void SpawnWave()
    {
        if (curentWave >= WaveSO.Length) return;
        if (countEnemy >= WaveSO[curentWave].numberOfEnemies)
        {
            countEnemy = 0;
            stopSpawn = true;
            return;
        };

        if (currentTime >= WaveSO[curentWave].spawnDelay && !stopSpawn)
        {
            SpawnRandom();
            currentTime = 0;
        }
    }
}
