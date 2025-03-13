using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class WaveSO : ScriptableObject
{
   public string waveName;
   public int numberOfEnemies;
   public float timeBetweenWave;
   public float spawnDelay;
}
