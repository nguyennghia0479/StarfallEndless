using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private WaveSO wave;
    private Transform[] waypoints;
    private Transform currentWaypoint;
    private int waypointIndex;
    private bool canMove;
    private readonly float sqrDistanceThreshold = .01f;

    private void Update()
    {
        HandleMovement();
    }

    public void SetupEnemy(WaveSO wave)
    {
        this.wave = wave;
        waypoints = wave.Waypoints;
        currentWaypoint = wave.GetStartingPoint();
        waypointIndex = 0;
        canMove = true;
    }

    private void HandleMovement()
    {
        if (wave == null || waypoints.Length <= 0 || !canMove)
            return;

        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if ((currentWaypoint.position - transform.position).sqrMagnitude < sqrDistanceThreshold)
        {
            waypointIndex++;
            if (waypointIndex < waypoints.Length)
                currentWaypoint = waypoints[waypointIndex];
            else
            {
                canMove = false;
                Destroy(gameObject);
            }
        }
    }
}
