using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyService
{
    public int currentCoins {  get; private set; }
    public int currentGems { get; private set; }
    public CurrencyService(CurrencyServiceScriptableObject currencyServiceSO) 
    {
        currentCoins = currencyServiceSO.startingCoins;
        currentGems = currencyServiceSO.startingGems;
    }

    public void Init() => NotifyUIOfCurrencyChange();

    public void SetCurrentCoins(int coins) { currentCoins = coins; }
    public void SetCurrentGems(int gems) {  currentGems = gems; }
    public void AdjustCoins(int coins) { currentCoins += coins; NotifyUIOfCurrencyChange(); }
    public void AdjustGems(int gems) { currentGems += gems; NotifyUIOfCurrencyChange(); }
    public bool HasEnoughGems(int gems) => currentGems >= gems;

    private void NotifyUIOfCurrencyChange() => GameService.Instance.EventService.onCurrencyUpdated?.Invoke(currentCoins, currentGems);
}
