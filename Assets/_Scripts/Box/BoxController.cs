using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxController : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    public float dropForce = 0.1f;
    public event Action OnBoxDestroyed;

    private void Start()
    {
        dropForce = 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") || 
            collision.gameObject.CompareTag("Sword") || 
            collision.gameObject.CompareTag("Bomb"))
        {
            Vector2 boxPosition = transform.position;
            gameObject.SetActive(false);
            SpawnRandomItem(boxPosition);
            OnBoxDestroyed?.Invoke();
            OnBoxDestroyed = null;
        }
    }

    private void SpawnRandomItem(Vector2 position)
    {
        int ramdomIndex = Random.Range(0, Items.Count);
        //SpawnerManager.Instance.SpawnObject(Items[ramdomIndex], position, Quaternion.identity);

       // GameObject spawnedItem = Instantiate(Items[ramdomIndex], position, Items[ramdomIndex].transform.rotation);
        GameObject spawnedItem = SpawnerManager.Instance.SpawnObject(Items[ramdomIndex], position, Items[ramdomIndex].transform.rotation);
        Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        if (rb != null)
        {
            Vector2 launchDirection = Random.insideUnitCircle.normalized * dropForce;
            if(spawnedItem.CompareTag("Arrow"))
            {
                int arrowCount = Random.Range(1, 6);
                for (int i = 0; i < arrowCount; i++)
                {
                    launchDirection = Random.insideUnitCircle.normalized * dropForce;
                    //GameObject arrow = Instantiate(spawnedItem, position, spawnedItem.transform.rotation);
                    GameObject arrow = SpawnerManager.Instance.SpawnObject(spawnedItem, position, spawnedItem.transform.rotation);
                    Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                    if (arrowRb != null)
                    {
                        arrowRb.linearVelocity = launchDirection;
                        arrowRb.AddTorque(Random.Range(-3f, 3f), ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                launchDirection = Random.insideUnitCircle.normalized * dropForce;
                rb.linearVelocity = launchDirection;
                rb.AddTorque(UnityEngine.Random.Range(-3f, 3f), ForceMode2D.Impulse);
            }
            
        }

        if(spawnedItem.CompareTag("Bomb"))
        {
            BombController bomb = spawnedItem.GetComponent<BombController>();
            if(bomb != null)
            {
                bomb.StartCountdown();
            }
        }
    }
}
