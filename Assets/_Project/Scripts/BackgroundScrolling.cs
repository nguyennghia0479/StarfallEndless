using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = .5f;

    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void Update()
    {
        material.mainTextureOffset += new Vector2(0, scrollSpeed) * Time.deltaTime;
    }
}
