using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIService : MonoBehaviour, IPointerDownHandler
{
    [Header("Buttons")]
    [SerializeField] private Button getChestButton;
    [SerializeField] private Button startUnlockingChestButton;
    [SerializeField] private Button unlockChestWithGemsButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button collectRewardsButton;

    [Header("Panels")]
    [SerializeField] private GameObject chestsPanel;
    [SerializeField] private GameObject openChestPanel;
    [SerializeField] private GameObject startUnlockingChestPanel;
    [SerializeField] private GameObject chestRewardsPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private TextMeshProUGUI gemsToUnlockText;
    [SerializeField] private TextMeshProUGUI popUpInfoText;
    [SerializeField] private TextMeshProUGUI coinsRewardText;
    [SerializeField] private TextMeshProUGUI gemsRewardText;


    private ChestView currentChestSelected;
    private const string chestUnlockedMessage = "CHEST UNLOCKED";
    private const string startedUnlockingChestMessage = "STARTED UNLOCKING CHEST";
    private const string chestQueuedMessage = "CHEST IS QUEUED";
    private const string notEnoughGemsMessage = "NOT ENOUGH GEMS";

    private void Awake()
    {
        SubscribeToEvents();
        InitializeTexts();
        InitializePanels();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        SubscribeToButtons();
        GameService.Instance.EventService.OnChestSetupComplete += AddChestToUI;
        GameService.Instance.EventService.OnLockedChestClicked += HandleLockedChestClicked;
        GameService.Instance.EventService.OnUnlockingChestClicked += HandleUnlockingChestClicked;
        GameService.Instance.EventService.OnUnlockedChestClicked += HandleUnlockedChestClicked;
        GameService.Instance.EventService.OnEmptyCanvasClicked += HandleEmptyCanvasClicked;
        GameService.Instance.EventService.OnStartUnlockingChestSuccessful += HandleChestUnlockingSuccessful;
        GameService.Instance.EventService.OnStartUnlockingChestFailed += HandleChestUnlockingFailed;
        GameService.Instance.EventService.OnChestUnlocked += HandleChestUnlocked;
        GameService.Instance.EventService.OnCurrencyUpdated += UpdateCurrency;
       
    }

    private void UnsubscribeFromEvents()
    {
        UnsubscribeFromButtons();
        GameService.Instance.EventService.OnChestSetupComplete -= AddChestToUI;
        GameService.Instance.EventService.OnLockedChestClicked -= HandleLockedChestClicked;
        GameService.Instance.EventService.OnEmptyCanvasClicked -= HandleEmptyCanvasClicked;
        GameService.Instance.EventService.OnStartUnlockingChestSuccessful -= HandleChestUnlockingSuccessful;
        GameService.Instance.EventService.OnStartUnlockingChestFailed -= HandleChestUnlockingFailed;
        GameService.Instance.EventService.OnChestUnlocked -= HandleChestUnlocked;
        GameService.Instance.EventService.OnCurrencyUpdated -= UpdateCurrency;
    }

    private void SubscribeToButtons()
    {
        getChestButton.onClick.AddListener(GenerateRandomChest);
        startUnlockingChestButton.onClick.AddListener(StartChestUnlock);
        unlockChestWithGemsButton.onClick.AddListener(OnChestOpenedWithGems);
        undoButton.onClick.AddListener(InitiateUndoFunctionality);
        collectRewardsButton.onClick.AddListener(CollectRewards);
    }

    private void UnsubscribeFromButtons()
    {
        getChestButton.onClick.RemoveAllListeners();
        startUnlockingChestButton.onClick.RemoveAllListeners();
        unlockChestWithGemsButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
        collectRewardsButton.onClick.RemoveAllListeners();
    }
    private void InitializeTexts()
    {
        coinsText.text = null;
        gemsText.text = null;
        popUpInfoText.text = null;
    }
    private void InitializePanels()
    {
        ToggleOpenChestPanel(false);
        ToggleStartUnlockingChestPanel(false);
        ToggleChestRewardsPanel(false);
    }
    private void ShowPopUpInfo(string text)
    {
        if (popUpInfoText.text != null)
            return;

        popUpInfoText.text = text;
        StartCoroutine(nameof(PopUpInfoCooldown));
    }

    private IEnumerator PopUpInfoCooldown()
    {
        yield return new WaitForSecondsRealtime(3f);
        popUpInfoText.text = null;
    }
    private void HandleChestUnlocked(ChestView view) => ShowPopUpInfo(chestUnlockedMessage);
    private void HandleChestUnlockingSuccessful() => ShowPopUpInfo(startedUnlockingChestMessage);
    private void HandleChestUnlockingFailed() => ShowPopUpInfo(chestQueuedMessage);
    private void GenerateRandomChest() => GameService.Instance.ChestService.GenerateRandomChest();
    private void ToggleOpenChestPanel(bool toggle) => openChestPanel.SetActive(toggle);
    private void ToggleStartUnlockingChestPanel(bool toggle) => startUnlockingChestPanel.SetActive(toggle);
    private void ToggleChestRewardsPanel(bool toggle) => chestRewardsPanel.SetActive(toggle);
    private void ToggleUndoButton(bool toggle) => undoButton.gameObject.SetActive(toggle);
    private void InitiateUndoFunctionality()
    {
        GameService.Instance.CommandService.CommandInvoker.UndoCommand();
    }
    private void OnChestOpenedWithGems()
    {
        int gems = currentChestSelected.controller.GetGemsToUnlock();
        if (!GameService.Instance.CurrencyService.HasEnoughGems(gems))
        {
            ShowPopUpInfo(notEnoughGemsMessage);
            return;
        }

        GameService.Instance.CurrencyService.AdjustGems(-gems);
        ToggleOpenChestPanel(false);
        ToggleUndoButton(true);
        CommandData commandData = CreateCommandData(gems);
        Command command = new UnlockChestCommand(commandData);
        GameService.Instance.ChestService.ProcessCommand(command);
    }

    private CommandData CreateCommandData(int gems)
    {
        CommandData commandData = new CommandData();
        commandData.SetChestView(currentChestSelected);
        commandData.SetGemsLost(gems);
        return commandData;
    }
    private void CollectRewards()
    {
        GameService.Instance.CurrencyService.AdjustCoins(currentChestSelected.coinsReward);
        GameService.Instance.CurrencyService.AdjustGems(currentChestSelected.gemsReward);
        GameService.Instance.ChestService.ReturnChestToPool(currentChestSelected);
        currentChestSelected.transform.SetParent(null);
        currentChestSelected = null;
        ToggleChestRewardsPanel(false);
    }
    private void HandleLockedChestClicked(ChestView view)
    {
        SetCurrentChestClicked(view);
        ToggleStartUnlockingChestPanel(true);
    }
    private void HandleUnlockingChestClicked(ChestView view)
    {
        SetCurrentChestClicked(view);
        UpdateOpenChestPanel();
    }
    private void HandleUnlockedChestClicked(ChestView view)
    {
        SetCurrentChestClicked(view);
        ToggleChestRewardsPanel(true);
        coinsRewardText.text = currentChestSelected.coinsReward.ToString();
        gemsRewardText.text = currentChestSelected.gemsReward.ToString();  
    }

    private void UpdateOpenChestPanel()
    {
        ToggleOpenChestPanel(true);
        gemsToUnlockText.text = currentChestSelected.controller.GetGemsToUnlock().ToString();
    }

    private void SetCurrentChestClicked(ChestView chestView) => currentChestSelected = chestView;
    private void HandleEmptyCanvasClicked()
    {
        this.currentChestSelected = null;
        InitializePanels();
    }
    private void StartChestUnlock()
    {
        GameService.Instance.ChestService.AddChestToWaitingQueue(currentChestSelected);
        ToggleStartUnlockingChestPanel(false);
    }
    public void AddChestToUI(ChestView chest) => chest.transform.SetParent(chestsPanel.transform);
    public void UpdateCurrency(int coins, int gems) { coinsText.text = coins.ToString(); gemsText.text = gems.ToString(); }
    public void OnPointerDown(PointerEventData eventData) => GameService.Instance.InputService.HandlePlayerClicked(eventData);
}
