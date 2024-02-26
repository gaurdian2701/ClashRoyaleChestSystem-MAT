using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private Button getChestButton;
    [SerializeField] private GameObject chestsPanel;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;

    private void Awake()
    {
        getChestButton.onClick.AddListener(GenerateRandomChest);
        UpdateCurrency(0, 0);
        GameService.Instance.EventService.onChestSetupComplete += AddChestToUI;
    }

    private void OnDestroy()
    {
        GameService.Instance.EventService.onChestSetupComplete -= AddChestToUI;
    }

    private void GenerateRandomChest() => GameService.Instance.ChestService.GenerateRandomChest();

    public void AddChestToUI(ChestView chest) => chest.transform.SetParent(chestsPanel.transform);
    public void UpdateCurrency(int coins, int gems) { coinsText.text = coins.ToString(); gemsText.text = gems.ToString(); }
}
