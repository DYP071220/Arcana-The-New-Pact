using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardDescriptionText;
    [SerializeField] private Text cardTitleText;
    [SerializeField] private Text actionPointText;
    [SerializeField] private CardMessage cardData;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private Warrior warriorPrefab;

    private Vector3 originalScale;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private int originalSiblingIndex;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
        if (rectTransform != null)
        {
            originalScale = rectTransform.localScale;
        }
        UpdateCardDisplay();
        if (cardManager == null)
        {
            cardManager = FindObjectOfType<CardManager>();
        }
    }

    private void UpdateCardDisplay()
    {
        if (cardData == null) return;
        cardImage.sprite = cardData.Card_Art;
        cardTitleText.text = cardData.Title;
        cardDescriptionText.text = cardData.Description;
        actionPointText.text = cardData.ActionPoint.ToString();
    }

    public CardMessage GetCardData()
    {
        return cardData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform == null || canvasGroup == null) return;

        rectTransform.localScale = originalScale * 1.7f;

        canvasGroup.alpha = 1f;
        originalSiblingIndex = rectTransform.GetSiblingIndex();
        rectTransform.SetAsLastSibling(); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform == null) return;
        rectTransform.localScale = originalScale;
        rectTransform.SetSiblingIndex(originalSiblingIndex);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnCardSelected();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnCardRightClicked();
        }
    }

    private void OnCardSelected()
    {
        Debug.Log($"使用卡牌: {cardData?.Title}");


        if (cardManager != null)
        {
            cardManager.RemoveCard(this.gameObject);
        }
    }


    private void OnCardRightClicked()
    {
        Debug.Log($"查看卡牌详情: {cardData?.Title}");
    }

    public void ResetCardState()
    {
        if (cardImage != null)
        {
            cardImage.color = Color.white;
        }

        if (rectTransform != null)
        {
            rectTransform.localScale = originalScale;
        }
    }


}