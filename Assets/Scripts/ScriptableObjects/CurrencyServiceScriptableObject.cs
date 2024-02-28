using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyServiceScriptableObject", menuName = "ScriptableObject/NewCurrencyService")]
public class CurrencyServiceScriptableObject : ScriptableObject
{
    public int startingCoins;
    public int startingGems;
}
