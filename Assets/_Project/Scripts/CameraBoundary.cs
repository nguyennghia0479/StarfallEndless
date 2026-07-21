using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float bottomPadding = 0;
    [Range(0f, 2f)]
    [SerializeField] private float topPadding = 0;
    [Range(0, 1f)]
    [SerializeField] private float topLimited = .5f;
    [Space]
    [SerializeField] private bool isLookUp;

    private Camera camera;
    private float spritePaddingX;
    private float spritePaddingY;
    private float heightClamp;
    private float widthClamp;

    private void Awake()
    {
        camera = Camera.main;
        heightClamp = camera.orthographicSize;
        widthClamp = heightClamp /  2;
    }

    public Vector3 ClampToCameraBoundaries(SpriteRenderer sprite, Vector3 targetPosition)
    {
        SetSpritePadding(sprite);

        Vector2 minBound = camera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBound = camera.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 midBound = camera.ViewportToWorldPoint(new Vector2(0, topLimited));

        float minXClamp = minBound.x + spritePaddingX;
        float maxXClamp = maxBound.x - spritePaddingX;
        float minYClamp = minBound.y + spritePaddingY + bottomPadding;
        float maxYClamp = midBound.y;

        if (!isLookUp)
        {
            minYClamp = midBound.y;
            maxYClamp = maxBound.y - spritePaddingY + topPadding;
        }
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXClamp, maxXClamp);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYClamp, maxYClamp);

        return targetPosition;
    }

    private void SetSpritePadding(SpriteRenderer sprite)
    {
        if (sprite != null)
        {
            spritePaddingX = sprite.bounds.extents.x;
            spritePaddingY = sprite.bounds.extents.y;
        }
    }

    public float GetHeightClamp() => heightClamp;
    public float GetWidthClamp() => widthClamp;
}
