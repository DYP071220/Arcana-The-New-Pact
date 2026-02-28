using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject NewCard;
    [SerializeField] private Transform cardCanves;
    [SerializeField] private GameManager gameManager;

    public GameObject[] handCards;
    public int handCardMAX = 10;
    public int nowHandCard = 0;

    private void Start()
    {
        handCards = new GameObject[handCardMAX];
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCard();
        }

    }
    public void AddCardTo(int number)
    {
        for(; nowHandCard  < number;)
        {
            AddCard();
        }
    }
    public void AddCard()
    {
        if (nowHandCard < handCardMAX)
        {
            GameObject card = Instantiate(NewCard, new Vector3(300, 300, 0), Quaternion.identity, cardCanves);
            handCards[nowHandCard] = card;
            nowHandCard++;
            SortCards();
        }
    }
    public void RemoveCard(GameObject cardToRemove)
    {
        for (int i = 0; i < nowHandCard; i++)
        {
            if (handCards[i] == cardToRemove)
            {
                RemoveCardAtIndex(i);
                break;
            }
        }
    }
    public void RemoveCardAtIndex(int index)
    {
        if (index < 0 || index >= nowHandCard) return;

        if (handCards[index] != null)
        {
            Destroy(handCards[index]);
        }

        if (index != nowHandCard - 1) 
        {
            handCards[index] = handCards[nowHandCard - 1];
        }
        handCards[nowHandCard - 1] = null;
        nowHandCard--;
        SortCards();
    }
    public void SortCards()
    {
        int number = nowHandCard;
        if (number == 0) return;
        //TODO 随手牌数量动态调整相对位置
        float space = 800f / (number + 1);
        float firstCardPosition = 500f - (space * (number - 1) / 2f);

        for (int i = 0; i < number; i++)
        {
            if (handCards[i] != null)
            {
                handCards[i].transform.position = new Vector3(firstCardPosition + i * space, 75, 0);
                handCards[i].GetComponent<RectTransform>().SetSiblingIndex(i);
            }
        }
    }
}
