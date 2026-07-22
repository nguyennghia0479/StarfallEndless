using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float selfDestroyTime = 2f;

    private float timer = 0;

    private void Update()
    {
        HandleMovement();
        SelfDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void HandleMovement()
    {
        transform.position += transform.up * (moveSpeed * Time.deltaTime);
    }

    private void SelfDestroy()
    {
        timer += Time.deltaTime;
        if (timer >= selfDestroyTime)
            Destroy(gameObject);
    }

}
