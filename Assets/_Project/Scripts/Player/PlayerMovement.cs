using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Boundary settings")]
    [SerializeField] private CameraBoundary cameraBoundary;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition =  transform.position + playerController.GetMoveDirection() * (moveSpeed * Time.deltaTime);
   
        transform.position = cameraBoundary.ClampToCameraBoundaries(sprite, targetPosition);
    }
}
