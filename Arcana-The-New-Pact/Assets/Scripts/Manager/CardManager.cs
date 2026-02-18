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
            SortCards();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveCard(0);
            SortCards();
        }
    }
    public void AddCardTo(int number)
    {
        for(; nowHandCard  < number;)
        {
            AddCard();
            SortCards();
        }
    }
    public void AddCard()
    {
        if (nowHandCard < handCardMAX)
        {
            GameObject card = Instantiate(NewCard, new Vector3(300, 300, 0), Quaternion.identity, cardCanves);
            handCards[nowHandCard] = card;
            nowHandCard++;
        }
    }
    public void RemoveCard(int index)
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
        float space = 600f / (number + 1);
        float firstCardPosition = 650f - (space * (number - 1) / 2f);

        for (int i = 0; i < number; i++)
        {
            if (handCards[i] != null)
            {
                handCards[i].transform.position = new Vector3(firstCardPosition + i * space, 75, 0);
            }
        }
    }
}
