using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public Transform player;
    public int tileSize = 10; // Kích thước mỗi vùng tile (10x10 ô)
    public int viewDistance = 4; // Số vùng tile sẽ được sinh ra xung quanh player

    private Dictionary<Vector3Int, GameObject> activeTiles = new Dictionary<Vector3Int, GameObject>(); // Lưu các tile đang hiển thị
    private Queue<GameObject> tilePool = new Queue<GameObject>(); // Pool chứa các tile không dùng

    void Start()
    {
        UpdateTiles();
    }

    void Update()
    {
        UpdateTiles();
    }

    void UpdateTiles()
    {
        Vector3Int playerPos = GetPlayerTilePosition();
        HashSet<Vector3Int> newTiles = new HashSet<Vector3Int>();

        // Tạo tile trong vùng hiển thị (xung quanh player)
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector3Int tilePos = new Vector3Int(
                    (playerPos.x / tileSize) * tileSize + x * tileSize,
                    (playerPos.y / tileSize) * tileSize + y * tileSize,
                    0
                );

                if (!activeTiles.ContainsKey(tilePos))
                {
                    GameObject tileChunk = GetTileChunk(tilePos);
                    activeTiles[tilePos] = tileChunk;
                }
                newTiles.Add(tilePos);
            }
        }

        // Xóa tile nằm ngoài vùng hiển thị
        List<Vector3Int> tilesToRemove = new List<Vector3Int>();
        foreach (var tile in activeTiles.Keys)
        {
            if (!newTiles.Contains(tile))
            {
                tilesToRemove.Add(tile);
            }
        }

        foreach (var tile in tilesToRemove)
        {
            ReturnTileChunk(tile);
        }
    }

    Vector3Int GetPlayerTilePosition()
    {
        Vector3 playerPos = player.position;
        return new Vector3Int(Mathf.FloorToInt(playerPos.x), Mathf.FloorToInt(playerPos.y), 0);
    }

    GameObject GetTileChunk(Vector3Int position)
    {
        GameObject tileChunk;
        if (tilePool.Count > 0)
        {
            tileChunk = tilePool.Dequeue();
        }
        else
        {
            tileChunk = new GameObject("TileChunk");
            tileChunk.transform.parent = transform;
        }

        tileChunk.transform.position = new Vector3(position.x, position.y, 0);
        tileChunk.SetActive(true);
        FillTilemap(position);
        return tileChunk;
    }

    void ReturnTileChunk(Vector3Int position)
    {
        if (activeTiles.ContainsKey(position))
        {
            GameObject tileChunk = activeTiles[position];
            tileChunk.SetActive(false);
            tilePool.Enqueue(tileChunk);
            ClearTilemap(position);
            activeTiles.Remove(position);
        }
    }

    void FillTilemap(Vector3Int position)
    {
        for (int x = 0; x < tileSize; x++)
        {
            for (int y = 0; y < tileSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(position.x + x, position.y + y, 0);
                tilemap.SetTile(tilePos, tile);
            }
        }
    }

    void ClearTilemap(Vector3Int position)
    {
        for (int x = 0; x < tileSize; x++)
        {
            for (int y = 0; y < tileSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(position.x + x, position.y + y, 0);
                tilemap.SetTile(tilePos, null);
            }
        }
    }
}
