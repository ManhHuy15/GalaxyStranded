using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSmallMap : MonoBehaviour
{
    [SerializeField] public List<GameObject> smallMapPrefabs; // Danh sách prefab map nhỏ
    [SerializeField] private Transform player; // Tham chiếu đến transform của người chơi
    [SerializeField] private float mapSize = 10f; // Kích thước của mỗi ô map
    [SerializeField] private float spawnDistance = 15f; // Khoảng cách từ rìa camera để sinh map
    [SerializeField] private float returnDistance = 20f; // Khoảng cách từ người chơi để trả map về pool
    [SerializeField] private int poolSize = 20; // Số lượng ô map tối đa trong pool

    private Dictionary<Vector2Int, GameObject> spawnedMaps = new Dictionary<Vector2Int, GameObject>(); // Các ô map đang hiển thị
    private Queue<GameObject> mapPool = new Queue<GameObject>(); // Pool để lưu các ô map không dùng
    private Vector2Int lastPlayerGridPos; // Vị trí lưới cuối cùng của người chơi
    private Camera mainCamera; // Tham chiếu đến camera chính

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null) Debug.LogError("Player not found! Please tag your player object with 'Player'.");
        }
        mainCamera = Camera.main;
        if (mainCamera == null) Debug.LogError("Main camera not found!");

        // Khởi tạo pool
        InitializePool();

        lastPlayerGridPos = GetGridPosition(player.position);
        SpawnMapsOutsideCamera(); // Sinh map ban đầu
    }

    void Update()
    {
        Vector2Int currentPlayerGridPos = GetGridPosition(player.position);

        if (currentPlayerGridPos != lastPlayerGridPos)
        {
            SpawnMapsOutsideCamera();
            CleanUpDistantMaps();
            lastPlayerGridPos = currentPlayerGridPos;
        }
    }

    // Khởi tạo pool với các ô map
    void InitializePool()
    {
        if (smallMapPrefabs == null || smallMapPrefabs.Count == 0)
        {
            Debug.LogError("smallMapPrefabs is empty! Please assign prefabs in the Inspector.");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            int random = Random.Range(0, smallMapPrefabs.Count);
            GameObject map = Instantiate(smallMapPrefabs[random], Vector3.zero, Quaternion.identity);
            map.SetActive(false);
            mapPool.Enqueue(map);
        }
        Debug.Log($"Pool initialized with {poolSize} maps.");
    }

    // Chuyển đổi vị trí thực tế sang vị trí lưới
    Vector2Int GetGridPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / mapSize);
        int y = Mathf.FloorToInt(position.y / mapSize);
        return new Vector2Int(x, y);
    }

    // Sinh map ngoài tầm nhìn của camera
    void SpawnMapsOutsideCamera()
    {
        Vector2Int playerGridPos = GetGridPosition(player.position);
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camPos = mainCamera.transform.position;

        // Vùng nhìn của camera
        float camLeft = camPos.x - camWidth;
        float camRight = camPos.x + camWidth;
        float camTop = camPos.y + camHeight;
        float camBottom = camPos.y - camHeight;

        // Vùng sinh map: cách rìa camera một khoảng spawnDistance
        int gridBuffer = Mathf.CeilToInt((camWidth + spawnDistance) / mapSize) + 1;

        for (int x = -gridBuffer; x <= gridBuffer; x++)
        {
            for (int y = -gridBuffer; y <= gridBuffer; y++)
            {
                Vector2Int gridPos = playerGridPos + new Vector2Int(x, y);
                Vector3 worldPos = new Vector3(gridPos.x * mapSize, gridPos.y * mapSize, 0);

                // Kiểm tra nếu ô nằm ngoài tầm nhìn camera và trong khoảng spawnDistance
                bool outsideCamera = (worldPos.x < camLeft || worldPos.x > camRight || worldPos.y > camTop || worldPos.y < camBottom);
                float distanceFromCameraEdge = Mathf.Min(
                    Mathf.Abs(worldPos.x - camLeft), Mathf.Abs(worldPos.x - camRight),
                    Mathf.Abs(worldPos.y - camTop), Mathf.Abs(worldPos.y - camBottom)
                );

                if (!spawnedMaps.ContainsKey(gridPos) && outsideCamera && distanceFromCameraEdge <= spawnDistance)
                {
                    if (mapPool.Count == 0)
                    {
                        Debug.LogWarning("Pool is empty! Increase poolSize or check map return logic.");
                        return;
                    }

                    GameObject map = mapPool.Dequeue();
                    map.transform.position = worldPos;
                    map.SetActive(true);
                    spawnedMaps.Add(gridPos, map);
                    Debug.Log($"Spawned map at {gridPos}");
                }
            }
        }
    }

    // Xóa các ô map xa người chơi và trả về pool
    void CleanUpDistantMaps()
    {
        List<Vector2Int> toRemove = new List<Vector2Int>();
        Vector2 playerPos = player.position;

        foreach (var map in spawnedMaps)
        {
            Vector3 mapPos = map.Value.transform.position;
            if (Vector2.Distance(playerPos, mapPos) > returnDistance)
            {
                toRemove.Add(map.Key);
                map.Value.SetActive(false);
                mapPool.Enqueue(map.Value);
                Debug.Log($"Returned map at {map.Key} to pool");
            }
        }

        foreach (var key in toRemove)
        {
            spawnedMaps.Remove(key);
        }
    }
}