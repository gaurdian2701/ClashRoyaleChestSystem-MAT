using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private Button getChestButton;
    [SerializeField] private GameObject chestsPanel;

    private void Awake()
    {
        getChestButton.onClick.AddListener(GenerateRandomChest);
        GameService.Instance.EventService.onChestSetupComplete += AddChestToUI;
    }

    private void OnDestroy()
    {
        GameService.Instance.EventService.onChestSetupComplete -= AddChestToUI;
    }

    private void GenerateRandomChest() => GameService.Instance.ChestService.GenerateRandomChest();

    public void AddChestToUI(ChestView chest) => chest.transform.SetParent(chestsPanel.transform);
}
