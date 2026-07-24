using UnityEngine;

public class Player : Entity
{
    [SerializeField] private SpriteRenderer shieldSpriteRenderer;

    public PlayerMovement Movement { get; private set; }

    private float enableShieldTimer;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<PlayerMovement>();
        Movement.Initialize(stats.MoveSpeed);
    }

    private void Update()
    {
        DisableShield();
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();

        GameEvents.RaisePlayerDestroyed(transform.position);
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
