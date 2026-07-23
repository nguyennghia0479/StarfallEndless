using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float lifeTime = 10f;
    [SerializeField] protected bool isMoveUp;

    protected float lifeTimer;
    protected bool isDestroyed;

    protected virtual void Update()
    {
        HandleMovement();
        SelfDestroy();
    }

    public virtual void Initialize(float moveSpeed) => this.moveSpeed = moveSpeed;

    protected virtual void HandleMovement()
    {
        float moveDelta = moveSpeed * Time.deltaTime;
        Vector3 targetPosition = isMoveUp ? transform.up * moveDelta : -transform.up * moveDelta;

        transform.position += targetPosition;
    }

    protected void SelfDestroy()
    {
        if (isDestroyed)
            return;

        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
