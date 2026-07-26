using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    [SerializeField] private MeteoriteDatabaseSO meteoriteDB;
    [SerializeField] private float minTimeToSpawn = 20f;
    [SerializeField] private float maxTimeToSpawn = 40f;

    [Header("Spawn position")]
    [SerializeField] private CameraBoundary cameraBoundary;
    [SerializeField] private Transform spawnPoint;

    private Meteorite[] meteorites;
    private float timeToSpawn;
    private float spawnTimer;
    private float widthClamp;
    private float topBound;
    private bool canSpawn;

    private void Awake()
    {
        meteorites = meteoriteDB.Meteorites;
    }

    private void OnEnable()
    {
        GameEvents.OnGameStarted += EnableSpawnMeteorites;
        GameEvents.OnGameRetry += ResetWave;
        GameEvents.OnPlayerDestroyed += DisableSpawnMeteorites;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStarted -= EnableSpawnMeteorites;
        GameEvents.OnGameRetry -= ResetWave;
        GameEvents.OnPlayerDestroyed -= DisableSpawnMeteorites;
    }

    private void Start()
    {
        widthClamp = cameraBoundary.GetWidthClamp();
        topBound = cameraBoundary.GetTopBoundPosition();
    }

    private void Update()
    {
        if (!canSpawn)
            return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn)
        {
            spawnTimer = 0;
            timeToSpawn = GetTimeToSpawnMeteorite();
            SpawnMeteorite();
        }
    }

    private void SpawnMeteorite()
    {
        Meteorite meteoritePrefab = meteorites[Random.Range(0, meteorites.Length)];
        SpriteRenderer meteoriteSprite = meteoritePrefab.GetComponentInChildren<SpriteRenderer>();
        Vector3 spawnPosition = GetSpawnPosition(meteoriteSprite);

        Meteorite newMeteorite = Instantiate(meteoritePrefab, spawnPosition, Quaternion.identity);
        newMeteorite.SetupMeteorite(topBound);
    }

    private Vector3 GetSpawnPosition(SpriteRenderer sprite)
    {
        float randomXPos = Random.Range(-widthClamp, widthClamp);
        Vector3 cameraPos = cameraBoundary.transform.position;
        Vector3 spawnPosition = new(cameraPos.x + randomXPos, spawnPoint.position.y, 0);

        return cameraBoundary.ClampToCameraWidthBoundary(sprite, spawnPosition);
    }

    private float GetTimeToSpawnMeteorite() => Random.Range(minTimeToSpawn, maxTimeToSpawn);

    public void EnableSpawnMeteorites()
    {
        canSpawn = true;
        timeToSpawn = GetTimeToSpawnMeteorite();
    }

    public void DisableSpawnMeteorites() => canSpawn = false;
    private void ResetWave() => spawnTimer = 0;
}
