using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Boundary settings")]
    [SerializeField] private CameraBoundary cameraBoundary;

    private PlayerController playerController;
    private float defaultMoveSpeed;
    private float buffTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        defaultMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleMovement();
        RemoveModifier();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition =  transform.position + playerController.GetMoveDirection() * (moveSpeed * Time.deltaTime);
   
        transform.position = cameraBoundary.ClampToCameraBoundaries(sprite, targetPosition);
    }

    public void AddModifier(float buffPercent, float duration)
    {
        buffTimer = duration;
        moveSpeed = defaultMoveSpeed + (defaultMoveSpeed * buffPercent);
    }

    private void RemoveModifier()
    {
        if (moveSpeed == defaultMoveSpeed)
            return;

        if (buffTimer <= 0)
        {
            moveSpeed = defaultMoveSpeed;
        }
    }
}
