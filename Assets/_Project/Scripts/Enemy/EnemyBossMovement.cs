using System;
using UnityEngine;

public class EnemyBossMovement : EnemyMovement
{
    public event Action OnEnteredScreen;

    [SerializeField] private CameraBoundary cameraBoundary;
    [SerializeField] private SpriteRenderer sprite;

    private HealthPoint healthPoint;
    private bool canRoamMove;
    private float heightClamp;
    private float widthClamp;
    private Vector3 targetPosition;
    private bool isEnterScreen = true;

    private void Awake()
    {
        healthPoint = GetComponent<HealthPoint>();
    }

    private void Start()
    {
        heightClamp = cameraBoundary.GetHeightClamp();
        widthClamp = cameraBoundary.GetWidthClamp();
        healthPoint.DisableDamaged();
    }

    protected override void Update()
    {
        HandleMoveToPosition(isEnterScreen);
        HandleRoamingMove();
    }

    protected void HandleMoveToPosition(bool isEnterScreen)
    {
        if (waypoints.Length <= 0 || !isMovedByWaypoint)
            return;

        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if ((currentWaypoint.position - transform.position).sqrMagnitude < sqrDistanceThreshold)
        {
            if (isEnterScreen)
                EnterScreen();
            else
                ExitScreen();
        }
    }

    private void EnterScreen()
    {
        waypointIndex++;
        if (waypointIndex < waypoints.Length)
            currentWaypoint = waypoints[waypointIndex];
        else
            ChangeToRoamingMove();
    }

    private void ExitScreen()
    {
        waypointIndex--;
        if (waypointIndex >= 0)
            currentWaypoint = waypoints[waypointIndex];
        else
            Destroy(gameObject);
    }

    public void StopRoamingMove()
    {
        isEnterScreen = false;
        isMovedByWaypoint = true;
        canRoamMove = false;
    }

    private void ChangeToRoamingMove()
    {
        OnEnteredScreen?.Invoke();
        isMovedByWaypoint = false;
        canRoamMove = true;
        targetPosition = GetRandomPosition();
        healthPoint.EnableDamaged();
    }

    private void HandleRoamingMove()
    {
        if (!canRoamMove) return;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if ((targetPosition - transform.position).sqrMagnitude < sqrDistanceThreshold)
        {
            targetPosition = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomXPos = UnityEngine.Random.Range(-widthClamp, widthClamp);
        float randomYPos = UnityEngine.Random.Range(-heightClamp, heightClamp);
        Vector3 cameraPos = cameraBoundary.transform.position;
        Vector3 rawPos = new(cameraPos.x + randomXPos, cameraPos.y + randomYPos, 0);

        return cameraBoundary.ClampToCameraBoundaries(sprite, rawPos);
    }
}
