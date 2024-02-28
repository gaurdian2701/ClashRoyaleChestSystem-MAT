using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObject/NewChest")]
public class ChestScriptableObject : ScriptableObject
{
    public ChestRarity ChestRarity;
    public Sprite ChestImage;
    public int LowerCoinLimit;
    public int UpperCoinLimit;
    public int LowerGemLimit;
    public int UpperGemLimit;
    public int WaitTime;
}
