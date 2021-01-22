using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<CardData> cardDataList = new List<CardData>();
}
