using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public struct PrefabInfo
{
    public GameObject prefab;
    public int defaultPoolSize;
    public int maxPoolSize;
    public Transform holder;
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance {  get; private set; }

    [SerializeField] private PrefabInfo[] prefabInfos;
    
    private Dictionary<GameObject, IObjectPool<GameObject>> poolDict = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        CreateNewPool();
    }

    public GameObject GetPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.ContainsKey(prefab))
        {
            Debug.Log("Prefab hasn't created");
            return null;
        }

        GameObject activeGameObject = poolDict[prefab].Get();
        activeGameObject.transform.SetPositionAndRotation(position, rotation);
        return activeGameObject;
    }

    private void CreateNewPool()
    {
        foreach (var info in prefabInfos)
        {
            IObjectPool<GameObject> pool = null;
            pool = new ObjectPool<GameObject>(
            createFunc: () => OnCreatePoolItem(info.prefab, info.holder, pool),
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolItem,
            collectionCheck: true,
            defaultCapacity: info.defaultPoolSize,
            maxSize: info.maxPoolSize
            );

            PreWarm(pool, info.defaultPoolSize);
            poolDict.Add(info.prefab, pool);
        }

    }

    private void PreWarm(IObjectPool<GameObject> pool, int defaultPoolSize)
    {
        GameObject[] preSpawns = new GameObject[defaultPoolSize];
        for (int i = 0; i <preSpawns.Length; i++)
            preSpawns[i] = pool.Get();
        
        for (int i = 0; i <preSpawns.Length; i++)
            pool.Release(preSpawns[i]);
    }

    private GameObject OnCreatePoolItem(GameObject gameObject, Transform holder, IObjectPool<GameObject> pool)
    {
        if (holder == null)
            holder = transform;

        GameObject newObject = Instantiate(gameObject, holder);
        newObject.GetComponent<PooledObject>().SetupObjectPool(pool);
        return newObject;
    }

    private void OnGetFromPool(GameObject projectile)
    {
        projectile.SetActive(true);
    }

    private void OnReleaseToPool(GameObject projectile)
    {
        projectile.SetActive(false);
    }

    private void OnDestroyPoolItem(GameObject projectile)
    {
        if (projectile != null)
            Destroy(projectile);
    }
}
