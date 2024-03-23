using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("SERVICES SCRIPTABLE OBJECTS")]
    [SerializeField] private ChestServiceScriptableObject chestServiceSO;
    [SerializeField] private CurrencyServiceScriptableObject currencyServiceSO;

    [Header("CHEST DATA")]
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private List<ChestScriptableObject> chestDataList;

    public InputService InputService {  get; private set; }
    public ChestService ChestService {  get; private set; }
    public EventService EventService {  get; private set; }
    public CommandService CommandService {  get; private set; }
    public CurrencyService CurrencyService {  get; private set; }

    public PoolService PoolService { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        EventService = new EventService();
        InputService = new InputService(raycaster);
        ChestService = new ChestService(chestPrefab, chestServiceSO, chestDataList);
        CommandService = new CommandService();
        CurrencyService = new CurrencyService(currencyServiceSO);
        PoolService = new PoolService(chestPrefab);
    }

    private void Start()
    {
        CurrencyService.Init();
    }
}
