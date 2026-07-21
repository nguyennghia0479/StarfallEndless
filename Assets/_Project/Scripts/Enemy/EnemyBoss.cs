using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private CameraBoundary cameraBoundary;
    [SerializeField] private SpriteRenderer sprite;

    private bool canRoamMove;
    private float heightClamp;
    private float widthClamp;
    private Vector3 targetPosition;

    private void Start()
    {
        heightClamp = cameraBoundary.GetHeightClamp();
        widthClamp = cameraBoundary.GetWidthClamp();
    }

    protected override void Update()
    {
        base.Update();

        HandleRoamingMove();
    }

    protected override void HandleMovement()
    {
        if (waypoints.Length <= 0 || !isMovedByWaypoint)
            return;

        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if ((currentWaypoint.position - transform.position).sqrMagnitude < sqrDistanceThreshold)
        {
            waypointIndex++;
            if (waypointIndex < waypoints.Length)
                currentWaypoint = waypoints[waypointIndex];
            else
            {
                isMovedByWaypoint = false;
                canRoamMove = true;
                targetPosition = GetRandomPosition();
            }
        }
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
        float randomXPos = Random.Range(-widthClamp, widthClamp);
        float randomYPos = Random.Range(-heightClamp, heightClamp);
        Vector3 cameraPos = cameraBoundary.transform.position;
        Vector3 rawPos = new(cameraPos.x + randomXPos, cameraPos.y + randomYPos, 0);

        return cameraBoundary.ClampToCameraBoundaries(sprite, rawPos);
    }
}
