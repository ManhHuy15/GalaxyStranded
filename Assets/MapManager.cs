using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] tilemapPrefabs;
    public Transform player;
    public Transform gridParent;

    private Vector2Int currentTilemap;
    private float tilemapSizeX, tilemapSizeY;
    private Dictionary<Vector2Int, GameObject> activeTilemaps = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        tilemapSizeX = cameraWidth;
        tilemapSizeY = cameraHeight;

        currentTilemap = Vector2Int.zero;
        SpawnTilemapArea(currentTilemap);
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
            SpawnTilemapArea(currentTilemap);
        }
    }

    void SpawnTilemapArea(Vector2Int centerPos)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
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
        GameObject tilemapPrefab = tilemapPrefabs[Random.Range(0, tilemapPrefabs.Length)];
        Vector3 spawnPos = new Vector3(
            Mathf.Round(tilemapPos.x * tilemapSizeX),
            Mathf.Round(tilemapPos.y * tilemapSizeY),
            0
        );

        GameObject newTilemap = Instantiate(tilemapPrefab, spawnPos, Quaternion.identity);
        newTilemap.transform.SetParent(gridParent, false);
        activeTilemaps.Add(tilemapPos, newTilemap);
    }
}
