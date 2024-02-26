using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Panels")]
    [SerializeField] private GameObject chestsPanel;
    [SerializeField] private GameObject openChestPanel;
    [SerializeField] private GameObject startUnlockingChestPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private TextMeshProUGUI gemsToUnlockText;


    private ChestView currentChestClicked;

    private void Awake()
    {
        SubscribeToEvents();
        ToggleOpenChestPanel(false);
        ToggleStartUnlockingChestPanel(false);
        UpdateCurrency(0, 0);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        getChestButton.onClick.AddListener(GenerateRandomChest);
        startUnlockingChestButton.onClick.AddListener(StartChestUnlock);
        GameService.Instance.EventService.onChestSetupComplete += AddChestToUI;
        GameService.Instance.EventService.onLockedChestClicked += HandleLockedChestClicked;
        GameService.Instance.EventService.onUnlockingChestClicked += HandleUnlockingChestClicked;
        GameService.Instance.EventService.onEmptyCanvasClicked += HandleEmptyCanvasClicked;
    }

    private void UnsubscribeFromEvents()
    {
        getChestButton.onClick.RemoveAllListeners();
        startUnlockingChestButton.onClick.RemoveAllListeners();
        GameService.Instance.EventService.onChestSetupComplete -= AddChestToUI;
        GameService.Instance.EventService.onLockedChestClicked -= HandleLockedChestClicked;
        GameService.Instance.EventService.onEmptyCanvasClicked -= HandleEmptyCanvasClicked;
    }

    private void GenerateRandomChest() => GameService.Instance.ChestService.GenerateRandomChest();
    private void ToggleOpenChestPanel(bool toggle) => openChestPanel.SetActive(toggle);
    private void ToggleStartUnlockingChestPanel(bool toggle) => startUnlockingChestPanel.SetActive(toggle);

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

    private void UpdateOpenChestPanel()
    {
        ToggleOpenChestPanel(true);
        gemsToUnlockText.text = currentChestClicked.controller.GetGemsToUnlock().ToString();
    }

    private void SetCurrentChestClicked(ChestView chestView) => currentChestClicked = chestView;
    private void HandleEmptyCanvasClicked()
    {
        this.currentChestClicked = null;
        ToggleStartUnlockingChestPanel(false);
        ToggleOpenChestPanel(false);
    }
    private void StartChestUnlock()
    {
        GameService.Instance.ChestService.StartUnlockingChest(currentChestClicked);
        ToggleStartUnlockingChestPanel(false);
    }

    public void AddChestToUI(ChestView chest) => chest.transform.SetParent(chestsPanel.transform);
    public void UpdateCurrency(int coins, int gems) { coinsText.text = coins.ToString(); gemsText.text = gems.ToString(); }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameService.Instance.InputService.HandlePlayerClicked(eventData);
    }
}
