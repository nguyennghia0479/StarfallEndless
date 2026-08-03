using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Boundary settings")]
    [SerializeField] private SpriteRenderer shipSr;
    [SerializeField] private CameraBoundary cameraBoundary;

    private PlayerController playerController;
    private TrailRenderer trail;
    private float moveSpeed;
    private float defaultMoveSpeed;
    private float buffTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        GetTrail();
    }

    private void OnEnable()
    {
        UIEvents.OnSelectedShipButtonClicked += HandleShipVisuals;
    }

    private void OnDisable()
    {
        UIEvents.OnSelectedShipButtonClicked -= HandleShipVisuals;
    }

    private void Update()
    {
        HandleMovement();
        RemoveBuffSpeed();
    }

    public void Initialize(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        defaultMoveSpeed = moveSpeed;
    }

    private void HandleShipVisuals(PlayerModel model, PlayerShipSO _)
    {
        shipSr = model.ShipSprite;
        GetTrail();
    }

    private void GetTrail()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        DisableTrail();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = transform.position + playerController.GetMoveDirection() * (moveSpeed * Time.deltaTime);

        transform.position = cameraBoundary.ClampToCameraBoundaries(shipSr, targetPosition);
    }

    public void StopBuff() => buffTimer = 0;

    public void AppylyBuffSpeed(float buffPercent, float duration)
    {
        buffTimer = duration;
        moveSpeed = defaultMoveSpeed + (defaultMoveSpeed * buffPercent);
        EnableTrail();
    }

    private void RemoveBuffSpeed()
    {
        if (moveSpeed == defaultMoveSpeed)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
        {
            moveSpeed = defaultMoveSpeed;
            DisableTrail();
        }
    }

    private void EnableTrail()
    {
        if (trail == null) 
            return;

        trail.gameObject.SetActive(true);
    }
    private void DisableTrail()
    {
        if (trail == null)
            return;

        trail.gameObject.SetActive(false);
    }

    public void EnableMovement() => playerController.EnableInputs();
    public void DisableMovement() => playerController.DisableInputs();
}
