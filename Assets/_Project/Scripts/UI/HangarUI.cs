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

    private void OnEnable()
    {
        if (gameManager == null)
            return;

        currentIndex = SaveData.LoadLastShipSelected();
        UpdateHangarUI();

        UIEvents.OnRewardChanged += UpdateRewardPointsText;

        previousButton.onClick.AddListener(OnPreviousButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        unlockButton.onClick.AddListener(OnUnlockButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        UIEvents.OnRewardChanged -= UpdateRewardPointsText;

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
        rewardPointsText.text = rewardPoints.ToString();
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
        damageStat.value = playerShip.ProjectileDamage / playerShip.MaxDamageRange;
        defendStat.value = playerShip.Defend / playerShip.MaxDefendRange;
        hpStat.value = playerShip.MaxHP / playerShip.MaxHPRange;
        speedStat.value = playerShip.MoveSpeed / playerShip.MaxSpeedRange;
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
        UIEvents.RaiseSelectedShipButtonClicked(newPlayerModel);
        player.UpdatePlayerStats(playerShip);
        SaveData.SaveSelectedShip(currentIndex);
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
