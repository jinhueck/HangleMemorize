using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<CardData> cardDataList = new List<CardData>();
}

[Serializable]
public struct CardData
{
    public string cardName;
    public string cardImageName;
    public int cardPos;
    public string usedTime;
}