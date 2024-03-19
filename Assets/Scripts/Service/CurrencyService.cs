
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
    public void AdjustCoins(int coins) { currentCoins += coins; NotifyUIOfCurrencyChange(); }
    public void AdjustGems(int gems) { currentGems += gems; NotifyUIOfCurrencyChange(); }
    public bool HasEnoughGems(int gems) => currentGems >= gems;

    private void NotifyUIOfCurrencyChange() => GameService.Instance.EventService.OnCurrencyUpdated?.Invoke(currentCoins, currentGems);
}