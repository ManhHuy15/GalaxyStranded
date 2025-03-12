using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class WaveSO : ScriptableObject
{
   public  int numberOfEnemies;
   public float timeBetweenWave;
   public float spawnDelay;
}
