using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DataState
{
    State_NewData,
    State_SaveData,
}

public class CardDataController : MonoBehaviour
{
    public ScriptableObjectUtility scriptableObjectUtility;
    public CSVReader csvReader;

    public List<CardData> cardDataList;
    public List<CardData> useCardDataList;

    public void SetCardData(int stateNum)
    {
        DataState state = (DataState)stateNum;
        CardScriptableObject csvSOFile = csvReader.GetCSVData();
        List<CardData> csvCardDataList = csvSOFile.cardDataList;
        CardScriptableObject saveSOFile = scriptableObjectUtility.GetAssetData<CardScriptableObject>();
        List<CardData> saveCardDataList = saveSOFile.cardDataList;


        switch(state)
        {
            case DataState.State_NewData:
                cardDataList = csvCardDataList;
                break;

            case DataState.State_SaveData:
                List<CardData> removeDataList = saveCardDataList.Except(csvCardDataList).ToList();
                cardDataList = saveCardDataList.Except(removeDataList).ToList();
                cardDataList.AddRange(csvCardDataList);
                cardDataList = cardDataList.Distinct().ToList();
                break;
        }
    }
}
