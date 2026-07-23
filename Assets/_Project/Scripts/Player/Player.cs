using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shieldSpriteRenderer;

    public HealthPoint Health {  get; private set; }
    public PlayerMovement Movement { get; private set; }
    public Shooter Shooter { get; private set; }

    private float enableShieldTimer;

    private void Awake()
    {
        Health = GetComponent<HealthPoint>();
        Movement = GetComponent<PlayerMovement>();
        Shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        Health.OnDestroyed += HandleDestroyed;
        Health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        Health.OnDestroyed -= HandleDestroyed;
        Health.OnDamaged -= HandleDamaged;
    }

    private void Update()
    {
        DisableShield();
    }

    private void HandleDestroyed()
    {
        GameEvents.RaiseExploded(transform.position);
        GameEvents.RaisePlayerDied();
    }

    private void HandleDamaged()
    {
        GameEvents.RaisePlayerDamaged();
    }

    public void EnableShield(Sprite shieldSprite, float duration)
    {
        enableShieldTimer = duration;
        shieldSpriteRenderer.sprite = shieldSprite;
        shieldSpriteRenderer.gameObject.SetActive(true);
    }

    private void DisableShield()
    {
        if (!shieldSpriteRenderer.gameObject.activeSelf)
            return;

        enableShieldTimer -= Time.deltaTime;
        if (enableShieldTimer <= 0)
            shieldSpriteRenderer.gameObject.SetActive(false);
    }
}
