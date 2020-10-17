using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Color32 normalColor = Color.white;
    [SerializeField] private Color32 hoverColor = Color.grey;
    [SerializeField] private Color32 downColor = Color.blue;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = downColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //nothing right now.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = hoverColor;
    }
}
