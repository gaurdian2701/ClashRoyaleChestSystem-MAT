using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("SERVICES")]
    [SerializeField] private ChestServiceScriptableObject chestServiceSO;
    [SerializeField] private CurrencyServiceScriptableObject currencyServiceSO;

    [Header("CHEST DATA")]
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private GraphicRaycaster raycaster;

    public InputService InputService;
    public ChestService ChestService;
    public EventService EventService;
    public CommandService CommandService;
    public CurrencyService CurrencyService;

    protected override void Awake()
    {
        base.Awake();

        EventService = new EventService();
        InputService = new InputService(raycaster);
        ChestService = new ChestService(chestPrefab, chestServiceSO);
        CommandService = new CommandService();
        CurrencyService = new CurrencyService(currencyServiceSO);
    }

    private void Start() => CurrencyService.Init();
}
