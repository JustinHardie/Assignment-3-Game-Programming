using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float ObstacleSpawnTimer = 2f;
    public float ObstacelSpeed = 1f;

    [Header("Small size jitter")]
    [Tooltip("Random ±% around the prefab's current scale. 0.10 = ±10%")]
    public float sizeJitter = 0.08f;   // try 0.05–0.12


    // --- Lock to Ground (simple) ---
    public bool snapToGround = true;   // turn off if you don't want snapping
    public float spawnYOffset = 0f;    // nudge up/down after snapping
    float groundTopY;
    bool hasGround;

    float timeUntilObstacleSpawn;

    void Start()
    {
        if (snapToGround)
        {
            var g = GameObject.FindWithTag("Ground");
            if (g)
            {
                var col = g.GetComponent<Collider2D>();
                groundTopY = col ? col.bounds.max.y : g.transform.position.y;
                hasGround = true;
            }
        }
    }

    void Update()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= ObstacleSpawnTimer)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
{
    GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    GameObject spawnedObstacle = Instantiate(prefab, transform.position, Quaternion.identity);

    // --- SIZE JITTER ---
    float factor = 1f + Random.Range(-sizeJitter, sizeJitter);
    spawnedObstacle.transform.localScale *= factor;

    // --- SNAP TO GROUND ---
    if (snapToGround && hasGround)
    {
        float bottomY = GetBottomY(spawnedObstacle);
        float targetBottom = groundTopY + spawnYOffset;
        float delta = targetBottom - bottomY;
        spawnedObstacle.transform.position += new Vector3(0f, delta, 0f);
    }

    // --- RIGIDBODY SAFE SETUP ---
    Rigidbody2D rb = spawnedObstacle.GetComponent<Rigidbody2D>();

    if (rb == null)
    {
        rb = spawnedObstacle.AddComponent<Rigidbody2D>();
        Debug.LogWarning($"[Spawner] Added Rigidbody2D to '{spawnedObstacle.name}' because it was missing.");
    }

    rb.bodyType = RigidbodyType2D.Dynamic;
    rb.gravityScale = 0f;
    rb.freezeRotation = true;

    // Use correct property: velocity (not linearVelocity)
    rb.linearVelocity = Vector2.left * ObstacelSpeed;

    // --- AUTO DESTROY ---
    Destroy(spawnedObstacle, 20f);
}



    float GetBottomY(GameObject go)
    {
        // Prefer collider bounds; fall back to sprite bounds
        var col = go.GetComponentInChildren<Collider2D>();
        if (col) return col.bounds.min.y;

        var sr = go.GetComponentInChildren<SpriteRenderer>();
        if (sr) return sr.bounds.min.y;

        return go.transform.position.y;
    }
}
