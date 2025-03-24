using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected WaveSO[] WaveSO;
    [SerializeField] protected Wave Wave;
    [SerializeField] protected List<GameObject> EnemyPrefabs;
    [SerializeField] protected List<Transform> points;
    [SerializeField] protected GameObject waveUI;
    [SerializeField] protected Text waveName;
    [SerializeField] protected Text waveText;

    private int curentWave = 0;
    private float currentTime;
    private bool stopSpawn = false;
    [SerializeField] private int countEnemy = 0;
    [SerializeField] private Text score;
    [SerializeField] private int currentScore = 0;

    void Start()
    {
        Wave = new Wave();
        Wave.numberOfEnemies = 5;
        Wave.spawnDelay = 2f;
        Wave.waveName = "Wave " + curentWave.ToString();
        Wave.timeBetweenWave = 5f;
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

        GameObject enemy = EnemyPrefabs[randomEnemy];
        if(enemy.name == "Slime") enemy.GetComponent<EmenyComtroller>().StopDistance = Random.Range(2f, 5f);
        else enemy.GetComponent<EmenyComtroller>().StopDistance = 1f;
        SpawnerManager.Instance.SpawnObject(EnemyPrefabs[randomEnemy], pos.position, Quaternion.identity, HanderEnemyDestroyed);
        countEnemy++;
    }

    private Transform GetRandomPoint()
    {
        int rand = Random.Range(0, this.points.Count);
        return this.points[rand];
    }

    private IEnumerator NextWaveRotine()
    {
        yield return new WaitForSeconds(Wave.timeBetweenWave);
        NextWave();
    }

    void NextWave()
    {
        curentWave++;
        Debug.Log("Wave " + curentWave);
        //if (curentWave < WaveSO.Length)
        //{
        UpdateWave();
        AnimationWaveChange();
        //}
    }

    void UpdateWave()
    {
        Wave.numberOfEnemies += 5 ;
        Wave.spawnDelay -= 0.2f;
        Wave.waveName = "Wave " + curentWave.ToString();
        Wave.timeBetweenWave += 10f;
    }

    void AnimationWaveChange()
    {
        Animator animator = waveUI.GetComponent<Animator>();
        waveName.text = Wave.waveName;
        waveText.gameObject.SetActive(false);
        waveText.text = "Wave " + curentWave.ToString();
        animator.SetBool("isNextWave", true);
        stopSpawn = true;
        StartCoroutine(AnimationWaveChangeRotine());
    }

    private IEnumerator AnimationWaveChangeRotine()
    {
        yield return new WaitForSeconds(5f);
        waveUI.GetComponent<Animator>().SetBool("isNextWave", false);
        stopSpawn = false;
        waveText.gameObject.SetActive(true);
        StartCoroutine(NextWaveRotine());
    }

    void SpawnWave()
    {
        //if (curentWave >= WaveSO.Length) return;
        if (countEnemy >= Wave.numberOfEnemies)
        {
            countEnemy = 0;
            stopSpawn = true;
            return;
        };

        if (currentTime >= Wave.spawnDelay && !stopSpawn)
        {
            SpawnRandom();
            currentTime = 0;
        }
    }

    void HanderEnemyDestroyed()
    {
        currentScore += 1;
        score.text = $"Score {currentScore}";
    }
}
