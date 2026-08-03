using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image previewShip;

    [Header("Text Elements")]
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text costText;

    [Header("Stats Elements")]
    [SerializeField] private Slider damageStat;
    [SerializeField] private Slider defendStat;
    [SerializeField] private Slider hpStat;
    [SerializeField] private Slider speedStat;
    [SerializeField] private float changeDuration = .5f;

    [Header("Button Elements")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button selectedButton;

    private GameManager gameManager;
    private PlayerDatabase playerDB;
    private PlayerShipSO playerShip;
    private int currentIndex;
    private int maxSelect;
    private int currentSelect;
    private Coroutine rewardPointsRoutine;
    private Coroutine damageStatRoutine;
    private Coroutine defendStatRoutine;
    private Coroutine hpStatRoutine;
    private Coroutine speedStatRoutine;

    private void OnEnable()
    {
        if (gameManager == null)
            return;

        currentIndex = SaveData.LoadLastShipSelected();
        UpdateHangarUI();

        //UIEvents.OnRewardChanged += UpdateRewardPointsText;
        previousButton.onClick.AddListener(OnPreviousButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        unlockButton.onClick.AddListener(OnUnlockButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        //UIEvents.OnRewardChanged -= UpdateRewardPointsText;
        previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
        nextButton.onClick.RemoveListener(OnNextButtonClicked);
        unlockButton.onClick.RemoveListener(OnUnlockButtonClick);
        selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameManager = GameManager.Instance;
        playerDB = gameManager.PlayerDB;
        maxSelect = playerDB.Players.Length;
        currentIndex = SaveData.LoadLastShipSelected();
        currentSelect = currentIndex;
        playerShip = playerDB.Players[currentIndex];
        UpdateHangarUI();
    }

    public void LoadLastSelectedShip()
    {
        if (gameManager == null)
            Initialize();

        OnSelectButtonClicked();
    }

    public void UpdateRewardPointsText(int rewardPoints)
    {
        if (!gameObject.activeSelf)
        {
            rewardPointsText.text = rewardPoints.ToString();
            return;
        }

        if (rewardPointsRoutine != null)
            StopCoroutine(rewardPointsRoutine);

        rewardPointsRoutine = StartCoroutine(ChangeRewadPointsRoutine(rewardPoints));
    }

    private IEnumerator ChangeRewadPointsRoutine(int targetValue)
    {
        float elapsedTime = 0;
        if (!int.TryParse(rewardPointsText.text, out int lastValue))
        {
            rewardPointsText.text = targetValue.ToString();
            yield break;
        }

        while (elapsedTime <= changeDuration)
        {
            float currentValue = Mathf.Lerp(lastValue, targetValue, Mathf.Clamp01(elapsedTime / changeDuration));
            rewardPointsText.text = Mathf.RoundToInt(currentValue).ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rewardPointsText.text = targetValue.ToString();
    }

    private void UpdateTextView()
    {
        orderText.text = (currentIndex + 1).ToString() + "/" + maxSelect.ToString();
        costText.text = playerShip.UnlockedCost.ToString();
    }

    private void UpdatePreviewShip()
    {
        playerShip = playerDB.Players[currentIndex];
        previewShip.sprite = playerShip.ShipModel.ShipSprite.sprite;
    }

    private void UpdateShipStats()
    {
        float damageValue = playerShip.ProjectileDamage / playerShip.MaxDamageRange;
        float defendValue = playerShip.Defend / playerShip.MaxDefendRange;
        float hpValue = playerShip.MaxHP / playerShip.MaxHPRange;
        float speedValue = playerShip.Speed / playerShip.MaxSpeedRange;

        ChangeStat(ref damageStatRoutine, damageStat, damageValue);
        ChangeStat(ref defendStatRoutine, defendStat, defendValue);
        ChangeStat(ref hpStatRoutine, hpStat, hpValue);
        ChangeStat(ref speedStatRoutine, speedStat, speedValue);
    }

    private void ChangeStat(ref Coroutine routine, Slider statSlider, float targetValue)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ChangeStatRoutine(statSlider, targetValue));
    }

    private IEnumerator ChangeStatRoutine(Slider statSlider, float targetValue)
    {
        float elapsedTime = 0;
        float startValue = statSlider.value;
        while (elapsedTime <= changeDuration)
        {
            statSlider.value = Mathf.Lerp(startValue, targetValue, Mathf.Clamp01(elapsedTime / changeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        statSlider.value = targetValue;
    }

    private void UpdateButton()
    {
        if (!gameManager.HasUnlockedShip(currentIndex))
        {
            int currentRewardPoints = UIManager.Instance.GetRewardPoints();

            unlockButton.interactable = currentRewardPoints >= playerShip.UnlockedCost;
            unlockButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            selectedButton.gameObject.SetActive(false);
            return;
        }

        if (currentIndex == currentSelect)
        {
            selectedButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            selectButton.gameObject.SetActive(true);
            selectedButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(false);
        }
    }

    private void UpdateHangarUI()
    {
        UpdateTextView();
        UpdatePreviewShip();
        UpdateShipStats();
        UpdateButton();
    }

    private void OnPreviousButtonClicked()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = maxSelect - 1;

        UpdateHangarUI();
        UIEvents.RaiseButtonClicked();
    }

    private void OnNextButtonClicked()
    {
        currentIndex++;
        if (currentIndex >= maxSelect)
            currentIndex = 0;

        UpdateHangarUI();
        UIEvents.RaiseButtonClicked();
    }

    private void OnUnlockButtonClick()
    {
        int currentRewardPoints = UIManager.Instance.GetRewardPoints();
        if (currentRewardPoints < playerShip.UnlockedCost)
            return;

        gameManager.UnlockPlayerShip(currentIndex);
        UIEvents.RaiseButtonClicked();
        UIEvents.RaiseUnlockShipButtonClicked(playerShip.UnlockedCost);
        UpdateButton();
        SaveData.SaveUnlockShip(currentIndex.ToString());
    }

    private void OnSelectButtonClicked()
    {
        PlayerModel model = player.GetComponentInChildren<PlayerModel>();
        if (model == null)
            return;

        currentSelect = currentIndex;
        UpdateButton();

        Destroy(model.gameObject);
        PlayerModel newPlayerModel = Instantiate(playerShip.ShipModel, player.transform.position, Quaternion.identity, player.transform);
        UIEvents.RaiseButtonClicked();
        UIEvents.RaiseSelectedShipButtonClicked(newPlayerModel, playerShip);
        //player.UpdatePlayerStats(playerShip);
        SaveData.SaveSelectedShip(currentIndex);
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
