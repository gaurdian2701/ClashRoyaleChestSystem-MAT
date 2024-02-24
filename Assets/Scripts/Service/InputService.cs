using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputService
{
    private GraphicRaycaster raycaster;
    private List<RaycastResult> raycastResult;

    public InputService(GraphicRaycaster raycaster)
    {
        this.raycaster = raycaster; 
        raycastResult = new List<RaycastResult>();
    }

    public void HandlePlayerClicked(PointerEventData eventData)
    {
        raycaster.Raycast(eventData, raycastResult);
        ChestView chest = raycastResult[0].gameObject.GetComponent<ChestView>();
        Debug.Log(chest);
        raycastResult.Clear();
    }
}
