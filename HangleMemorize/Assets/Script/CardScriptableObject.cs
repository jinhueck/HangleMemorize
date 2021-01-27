using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardScriptableObject
{
    [SerializeField]
    public List<CardData> cardDataList = new List<CardData>();
}

[Serializable]
public class CardData
{
    public string cardName;
    public string cardImageName;
    public string cardInitialName;
    public int cardPos;
    public string usedTime;

    public CardData()
    {

    }

    public CardData(CardData cardData)
    {
        cardName = cardData.cardName;
        cardImageName = cardData.cardImageName;
        cardInitialName = cardData.cardInitialName;
        cardPos = 0;
        usedTime = "";
    }
}