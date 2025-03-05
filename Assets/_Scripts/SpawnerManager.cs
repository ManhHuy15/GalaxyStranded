using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> poolingObjects;

    private static SpawnerManager instance;
    public static SpawnerManager Instance => instance;
    void Start()
    {
        instance = this;
    }
    private GameObject GetGameObject(GameObject prefab)
    {
        foreach (var p in this.poolingObjects)
        {
            if (!p.activeSelf &&  p.name == prefab.name)
            {
                p.SetActive(true);
                return p;
            }
        }

        GameObject newPrefabs = Instantiate(prefab);
        newPrefabs.name = prefab.name;
        this.poolingObjects.Add(newPrefabs);
        newPrefabs.transform.parent = this.transform;
        return newPrefabs;
    }

    public void SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Action callback = null)
    {
        GameObject gameObject = GetGameObject(prefab);

        // su dung differernt enemy

        if (prefab.gameObject.tag == "Enemy")
        {
            //gameObject.GetComponent<EnemyController>().Destroyhandler += callback;
        }
        else if (prefab.gameObject.tag == "EnemySnipper")
        {
            //gameObject.GetComponent<EnemySnipperController>().Destroyhandler += callback;
        }


        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
    }
}
