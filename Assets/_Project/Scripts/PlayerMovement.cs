using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Boundary settings")]
    [Range(0f, 2f)]
    [SerializeField] private float bottomUIPading = 0;
    [Range(0, 1f)]
    [SerializeField] private float topLimited = .5f;

    private PlayerController playerController;
    private Camera camera;
    private float spritePaddingX;
    private float spritePaddingY;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        camera = Camera.main;

        SetSpritePadding();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 targetPosition =  transform.position + playerController.GetMoveDirection() * (moveSpeed * Time.deltaTime);
   
        transform.position = ClampToCameraBoundaries(targetPosition);
    }

    private Vector3 ClampToCameraBoundaries(Vector2 targetPosition)
    {
        Vector2 minBound = camera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBound = camera.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 midBound = camera.ViewportToWorldPoint(new Vector2(1, topLimited));

        float minXClamp = minBound.x + spritePaddingX;
        float maxXClamp = maxBound.x - spritePaddingX;
        float minYClamp = minBound.y + spritePaddingY + bottomUIPading;
        float maxYClamp = midBound.y;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minXClamp, maxXClamp);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYClamp, maxYClamp);

        return targetPosition;
    }

    private void SetSpritePadding()
    {
        if (sprite != null)
        {
            spritePaddingX = sprite.bounds.extents.x;
            spritePaddingY = sprite.bounds.extents.y;
        }
    }
}
