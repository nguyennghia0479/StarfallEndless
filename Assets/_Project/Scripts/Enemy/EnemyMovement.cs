using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected WaveSO wave;
    protected Transform[] waypoints;
    protected Transform currentWaypoint;
    protected int waypointIndex;
    protected bool isMovedByWaypoint;
    protected float moveSpeed = 6f;
    protected readonly float sqrDistanceThreshold = .01f;

    protected virtual void Update()
    {
        HandleMovement();
    }

    public void Initialize(float moveSpeed) => this.moveSpeed = moveSpeed;

    public virtual void SetupEnemyMove(WaveSO wave)
    {
        this.wave = wave;
        waypoints = wave.Waypoints;
        currentWaypoint = wave.GetStartingPoint();
        waypointIndex = 0;
        isMovedByWaypoint = true;
        transform.Rotate(180, 0, 0);
    }

    protected virtual void HandleMovement()
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
                Destroy(gameObject);
            }
        }
    }
}
