using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockedState : IStateInterface
{
    private ChestController controller;
    public ChestUnlockedState(ChestController controller) { this.controller = controller; }
    public override void OnStateEnter()
    {
        GameService.Instance.EventService.InvokeChestUnlockedEvent(controller.ChestView);
        controller.ChestView.SetChestStateText(ChestState.UNLOCKED);
        GenerateRewards();
    }

    public override void HandleClickEvent() => GameService.Instance.EventService.InvokeUnlockedChestClickedEvent(controller.ChestView);
    private void GenerateRewards()
    {
        System.Random rand = new System.Random();
        int coinsReward = rand.Next(controller.ChestData.LowerCoinLimit, controller.ChestData.UpperCoinLimit);
        int gemsReward = rand.Next(controller.ChestData.LowerGemLimit, controller.ChestData.UpperGemLimit);
        controller.SetRewards(coinsReward, gemsReward);
    }
}
