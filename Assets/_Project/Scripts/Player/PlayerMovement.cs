using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Boundary settings")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private CameraBoundary cameraBoundary;

    private PlayerController playerController;
    private float moveSpeed;
    private float defaultMoveSpeed;
    private float buffTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>(); 
    }

    private void Update()
    {
        HandleMovement();
        RemoveIncreaseSpeed();
    }

    public void Initialize(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        defaultMoveSpeed = moveSpeed;
    }

    private void HandleMovement()
    {
        Vector3 targetPosition =  transform.position + playerController.GetMoveDirection() * (moveSpeed * Time.deltaTime);
   
        transform.position = cameraBoundary.ClampToCameraBoundaries(sprite, targetPosition);
    }

    public void AppylyIncreaseSpeed(float buffPercent, float duration)
    {
        buffTimer = duration;
        moveSpeed = defaultMoveSpeed + (defaultMoveSpeed * buffPercent);
    }

    private void RemoveIncreaseSpeed()
    {
        if (moveSpeed == defaultMoveSpeed)
            return;

        if (buffTimer <= 0)
        {
            moveSpeed = defaultMoveSpeed;
        }
    }
}
