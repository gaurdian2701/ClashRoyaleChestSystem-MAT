using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameService : MonoBehaviour, IPointerClickHandler
{
    private InputService inputService;
    private GraphicRaycaster raycaster;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        inputService = new InputService(raycaster);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        inputService.HandlePlayerClicked(eventData);
    }
}
