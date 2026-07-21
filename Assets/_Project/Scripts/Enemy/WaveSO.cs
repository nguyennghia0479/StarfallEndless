using UnityEngine;

[CreateAssetMenu(fileName = "Wave ", menuName = "ScriptableObject/Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField] private Transform path;
    [SerializeField] private float minTimeToSpawn = .3f;
    [SerializeField] private float maxTimeToSpawn = .7f;

    private Transform[] waypoints;

    private void OnEnable()
    {
        waypoints = new Transform[path.childCount];

        for (int i = 0; i < path.childCount; i++)
            waypoints[i] = path.GetChild(i).transform;
    }

    public Transform[] Waypoints => waypoints;
    public Transform GetStartingPoint() => waypoints[0];
    public float GetTimeToSpawnEnemy() => Random.Range(minTimeToSpawn, maxTimeToSpawn);
}
