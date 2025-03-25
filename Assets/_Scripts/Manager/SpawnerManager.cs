using System;
using System.Collections.Generic;
using TMPro;
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
    private GameObject GetGameObject(GameObject prefab,GameObject parent)
    {
        foreach (var p in this.poolingObjects)
        {
            if (!p.activeSelf && p.name == prefab.name)
            {
                p.SetActive(true);
                return p;
            }
        }

        GameObject newPrefabs = Instantiate(prefab);
        newPrefabs.name = prefab.name;
        this.poolingObjects.Add(newPrefabs);

        if (parent != null) newPrefabs.transform.SetParent(parent.transform);
        else newPrefabs.transform.parent = this.transform;

        return newPrefabs;
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Action callback = null, GameObject parent = null)
    {
        GameObject gameObject = GetGameObject(prefab, parent);

        // su dung differernt enemy

        if (prefab.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EmenyComtroller>().OnEnemyDestroyed += callback;
        }
        else if (prefab.gameObject.tag == "DameText")
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = prefab.GetComponent<TextMeshProUGUI>().text;
        }
        else if (prefab.gameObject.tag == "Box")
        {
            gameObject.GetComponent<BoxController>().OnBoxDestroyed += callback;
        }


        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        return gameObject;
    }
}
