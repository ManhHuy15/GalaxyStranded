using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    public float dropForce = 0.5f;
    public static event Action OnBoxDestroyed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") || collision.gameObject.CompareTag("Sword"))
        {
            Debug.Log("Atack box secret");
            Vector2 boxPosition = transform.position;
            Destroy(gameObject);
            SpawnRandomItem(boxPosition);
            OnBoxDestroyed?.Invoke();
        }
    }

    private void SpawnRandomItem(Vector2 position)
    {
        int ramdomIndex = UnityEngine.Random.Range(0, Items.Count);
        //SpawnerManager.Instance.SpawnObject(Items[ramdomIndex], position, Quaternion.identity);

        GameObject spawnedItem = Instantiate(Items[ramdomIndex], position, Items[ramdomIndex].transform.rotation);
        Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        if (rb != null)
        {
            Vector2 launchDirection = UnityEngine.Random.insideUnitCircle.normalized * dropForce;
            rb.linearVelocity = launchDirection;
            rb.AddTorque(UnityEngine.Random.Range(-3f, 3f), ForceMode2D.Impulse);
        }
    }
}
