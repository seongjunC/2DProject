using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    private Card cardData;

    public Image cardSprite;
    public Image selectionOutline;

    [SerializeField] private bool IsSelected = false;
    [SerializeField] private RectTransform rectT;

    public void SetCard(Card card)
    {
        cardData = card;
        cardSprite.sprite = card.CardSprite;
        selectionOutline.enabled = false;
        rectT = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelected = !IsSelected;
        UIManager.instance.OnCardSelected(cardData, this);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        selectionOutline.enabled = selected;

        RectTransform rt = GetComponent<RectTransform>();
        if (selected)
        {
            rt.anchoredPosition = rectT.anchoredPosition + new Vector2(0, rt.rect.height * 0.5f);
        }
        else
        {
            rt.anchoredPosition = rectT.anchoredPosition - new Vector2(0, rt.rect.height * 0.5f);
        }
    }
}
