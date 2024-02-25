using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameService : GenericMonoSingleton<GameService>, IPointerClickHandler
{
    [Header("CHEST DATA")]
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private ChestServiceScriptableObject chestServiceSO;

    public InputService InputService;
    public ChestService ChestService;
    public EventService EventService;

    private GraphicRaycaster raycaster;

    protected override void Awake()
    {
        base.Awake();

        raycaster = GetComponent<GraphicRaycaster>();
        InputService = new InputService(raycaster);
        ChestService = new ChestService(chestPrefab, chestServiceSO);
        EventService = new EventService();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InputService.HandlePlayerClicked(eventData);
    }
}
