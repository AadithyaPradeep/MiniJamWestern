using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardUI;
    public Transform cardPosOne;
    public Transform cardPosTwo;
    public Transform cardPosThree;
    public List<Card> deck;
    public float comRar;
    public float rareRar;
    public float epicRar;
    public float legRar;
    public float bossRar;

    GameObject cardOne, cardTwo, cardThree;

    
 

    List<Card> alreadyselectedCards = new List<Card>();

    private void Start()
    {
        RandomizeNewCards();
    }
    void RandomizeNewCards()
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        // Copy deck into availableCards first
        List<Card> availableCards = new List<Card>(deck);

        // Remove unique cards that were already selected before
        availableCards.RemoveAll(card => card.isUnique && alreadyselectedCards.Contains(card));

        if (availableCards.Count < 3)
            return;

        List<Card> randomizedCards = GetWeightedRandomCards(availableCards, 3);

        if (randomizedCards.Count < 3)
        {
            return;
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPosOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPosTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPosThree);

    }
    List<Card> GetWeightedRandomCards(List<Card> source, int count)
    {
        List<Card> pool = new List<Card>(source);
        List<Card> result = new List<Card>();

        for (int i = 0; i < count; i++)
        {
            Card selected = GetWeightedRandomCard(pool);
            if (selected == null)
                break;

            result.Add(selected);
            pool.Remove(selected); // prevents duplicates in this roll

            // If unique, remember it permanently
            if (selected.isUnique && !alreadyselectedCards.Contains(selected))
            {
                alreadyselectedCards.Add(selected);
            }
        }

        return result;
    }

    Card GetWeightedRandomCard(List<Card> cards)
    {
        if (cards == null || cards.Count == 0)
            return null;

        float totalWeight = 0f;

        foreach (Card card in cards)
        {
            totalWeight += Mathf.Max(0f, card.rarity);
        }

        if (totalWeight <= 0f)
            return cards[Random.Range(0, cards.Count)];

        float randomPoint = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (Card card in cards)
        {
            currentWeight += Mathf.Max(0f, card.rarity);

            if (randomPoint <= currentWeight)
                return card;
        }

        return cards[cards.Count - 1];
    }
    public GameObject InstantiateCard(Card card, Transform position)
    {
        GameObject cardGo = Instantiate(card.gameObject, position.position, Quaternion.identity);
        return cardGo;
    }
}
