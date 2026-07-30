using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private IObjectPool<GameObject> pool;

    public void SetupObjectPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    public IObjectPool<GameObject> Pool => pool;
}
