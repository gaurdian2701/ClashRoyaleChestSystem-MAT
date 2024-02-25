using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameService : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Elements")]
    [SerializeField] private Button getChestButton;
    [SerializeField] private ChestView chestPrefab;

    public InputService InputService;
    public ChestService ChestService;

    private GraphicRaycaster raycaster;

    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        InputService = new InputService(raycaster);
        ChestService = new ChestService(chestPrefab);

        getChestButton.onClick.AddListener(GenerateRandomChest);
    }

    private void GenerateRandomChest() => ChestService.GenerateRandomChest();

    public void OnPointerClick(PointerEventData eventData)
    {
        InputService.HandlePlayerClicked(eventData);
    }
}
