using System.Collections;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = .2f;

    [Header("Change Backgrounds")]
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private SpriteRenderer currentBG;
    [SerializeField] private SpriteRenderer nextBG;
    [SerializeField] private float changeCooldown = 60f;
    [SerializeField] private float changeDuration = 3f;

    private MaterialPropertyBlock propertyBlock;
    private Material currentMat;
    private Material nextMat;
    private int currentBGIndex;
    private int nextBGIndex;
    private float changeTimer = 0;
    private bool isChangingBG;

    private void Awake()
    {
        propertyBlock = new();
        currentMat = currentBG.material;
        nextMat = nextBG.material;

        currentBGIndex = GetRandomBackground();
        currentBG.sprite = backgrounds[currentBGIndex];
        SetBackgroundMaterial(nextBG, 0);
    }

    private void Update()
    {
        ScrollingBackground();
        CheckChangeBackgroundIfNeed();
    }

    private void ScrollingBackground()
    {
        currentMat.mainTextureOffset += new Vector2(0, scrollSpeed) * Time.deltaTime;
        nextMat.mainTextureOffset += new Vector2(0, scrollSpeed) * Time.deltaTime;
    }

    private void CheckChangeBackgroundIfNeed()
    {
        changeTimer += Time.deltaTime;
        if (changeTimer >= changeCooldown)
        {
            changeTimer = 0;
            ChangeBackground();
        }
    }

    private void ChangeBackground()
    {
        if (isChangingBG || backgrounds == null || backgrounds.Length <= 1) 
            return;

        do
            nextBGIndex = GetRandomBackground();
        while (currentBGIndex == nextBGIndex);

        currentBGIndex = nextBGIndex;
        nextBG.sprite = backgrounds[nextBGIndex];
        SetBackgroundMaterial(nextBG, 1);
        StartCoroutine(ChangeBackgroundRoutine());
        
    }

    private IEnumerator ChangeBackgroundRoutine()
    {
        isChangingBG = true;
        float time = 0;

        while (time < changeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / changeDuration);
            SetBackgroundMaterial(currentBG, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        SetBackgroundMaterial(currentBG, 0);
        currentBG.sprite = nextBG.sprite;

        SetBackgroundMaterial(currentBG, 1);
        SetBackgroundMaterial(nextBG, 0);
        isChangingBG = false;
    }

    private void SetBackgroundMaterial(SpriteRenderer sprite, float targetAlpha)
    {
        targetAlpha = Mathf.Clamp01(targetAlpha);
        sprite.GetPropertyBlock(propertyBlock);

        Color color = new(1f, 1f, 1f, targetAlpha);
        propertyBlock.SetColor(SharderIDs.BASE_COLOR, color);
        sprite.SetPropertyBlock(propertyBlock);
    }

    private int GetRandomBackground() => Random.Range(0, backgrounds.Length);
}
