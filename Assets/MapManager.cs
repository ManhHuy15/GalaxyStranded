using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public GameObject[] tilemapPrefabs;
    public Transform player;
    public Transform gridParent;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private int poolSize = 25;
    private Vector2Int currentTilemap;
    private float tilemapSizeX, tilemapSizeY;
    private Dictionary<Vector2Int, GameObject> activeTilemaps = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> tilemapsToRemove = new List<Vector2Int>();

    private int spawnRange = 1;  // Phạm vi sinh tilemap (có thể thay đổi)
    private int removeRange = 2; // Phạm vi xóa tilemap (có thể thay đổi)

    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        tilemapSizeX = cameraWidth;
        tilemapSizeY = cameraHeight;

        InitializePool();
        currentTilemap = Vector2Int.zero;
        UpdateTilemapArea(currentTilemap);
    }

    void Update()
    {
        Vector2 playerPos = player.position;
        Vector2Int newTilemap = new Vector2Int(
            Mathf.FloorToInt(playerPos.x / tilemapSizeX),
            Mathf.FloorToInt(playerPos.y / tilemapSizeY)
        );

        if (newTilemap != currentTilemap)
        {
            currentTilemap = newTilemap;
            UpdateTilemapArea(currentTilemap);
        }
    }

    public void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tilemapPrefab = tilemapPrefabs[Random.Range(0, tilemapPrefabs.Length)];
            GameObject newTilemap = Instantiate(tilemapPrefab, Vector3.zero, Quaternion.identity);
            newTilemap.transform.SetParent(gridParent, false);
            newTilemap.SetActive(false);
            pool.Enqueue(newTilemap);
        }
    }

    public GameObject GetGameObjectFromPool()
    {
        if (pool.Count == 0)
        {
            GameObject tilemapPrefab = tilemapPrefabs[Random.Range(0, tilemapPrefabs.Length)];
            GameObject newTilemap = Instantiate(tilemapPrefab, Vector3.zero, Quaternion.identity);
            newTilemap.transform.SetParent(gridParent, false);
            newTilemap.SetActive(false);
            return newTilemap;
        }
        return pool.Dequeue();
    }

    void UpdateTilemapArea(Vector2Int centerPos)
    {
        tilemapsToRemove.Clear();
        foreach (var tilemap in activeTilemaps)
        {
            Vector2Int pos = tilemap.Key;
            if (Mathf.Abs(pos.x - centerPos.x) > removeRange || Mathf.Abs(pos.y - centerPos.y) > removeRange)
            {
                tilemapsToRemove.Add(pos);
            }
        }

        foreach (Vector2Int pos in tilemapsToRemove)
        {
            GameObject tilemap = activeTilemaps[pos];
            activeTilemaps.Remove(pos);
            tilemap.SetActive(false);
            pool.Enqueue(tilemap);
        }

        // Sử dụng spawnRange để xác định phạm vi sinh tilemap
        for (int x = -spawnRange; x <= spawnRange; x++)
        {
            for (int y = -spawnRange; y <= spawnRange; y++)
            {
                Vector2Int tilePos = new Vector2Int(centerPos.x + x, centerPos.y + y);
                if (!activeTilemaps.ContainsKey(tilePos))
                {
                    SpawnRandomTilemap(tilePos);
                }
            }
        }
    }

    void SpawnRandomTilemap(Vector2Int tilemapPos)
    {
        Vector3 spawnPos = new Vector3(
            Mathf.Round(tilemapPos.x * tilemapSizeX),
            Mathf.Round(tilemapPos.y * tilemapSizeY),
            10
        );

        GameObject tilemap = GetGameObjectFromPool();
        tilemap.transform.position = spawnPos;
        tilemap.SetActive(true);
        activeTilemaps.Add(tilemapPos, tilemap);
    }
}
