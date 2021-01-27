using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Hardware;
using UnityEngine;

public enum DataState
{
    State_NewData,
    State_SaveData,
}

public class CardDataController : MonoBehaviour
{
    public JsonController jsonController;
    public string jsonFilePath;
    public string jsonFileName;
    //public ScriptableObjectUtility scriptableObjectUtility;
    public CSVReader csvReader;

    public List<CardData> cardDataList;
    public List<CardData> useCardDataList;

    public void SetCardData(int stateNum)
    {
        DataState state = (DataState)stateNum;
        CardScriptableObject csvSOFile = csvReader.GetCSVData();
        List<CardData> csvCardDataList = csvSOFile.cardDataList;
        //CardScriptableObject saveSOFile = scriptableObjectUtility.GetAssetData<CardScriptableObject>();
        CardScriptableObject saveSOFile = jsonController.LoadJsonFile<CardScriptableObject>(jsonFilePath, jsonFileName);
        List<CardData> saveCardDataList = null;
        if (saveSOFile != null)
        {
            saveCardDataList = saveSOFile.cardDataList;
        }
        else
        {
            state = DataState.State_NewData;
        }


        switch (state)
        {
            case DataState.State_NewData:
                cardDataList = csvCardDataList;
                break;

            case DataState.State_SaveData:
                cardDataList = csvCardDataList;
                foreach (var saveCardData in saveCardDataList)
                {
                    foreach (var cardData in cardDataList)
                    {
                        if(cardData.cardName == saveCardData.cardName)
                        {
                            cardData.cardPos = saveCardData.cardPos;
                            cardData.usedTime = saveCardData.usedTime;
                        }
                    }   
                }
                break;
        }
    }

    public void SaveCardData()
    {
        CardScriptableObject saveCardSOFile = new CardScriptableObject();
        foreach (var cardData in cardDataList)
        {
            saveCardSOFile.cardDataList.Add(cardData);
        }
        string nJson = jsonController.ObjectToJson<CardScriptableObject>(saveCardSOFile);
        jsonController.CreateJsonFile(jsonFilePath, jsonFileName, nJson);
        //scriptableObjectUtility.CreateAsset<CardScriptableObject>(saveCardSOFile);
    }

    public bool CheckGameEnd()
    {
        int totalCount = cardDataList.Count;
        var endCardDataList = cardDataList.Where(x => x.cardPos == 5).ToList();
        int endCardDataCount = endCardDataList.Count;

        return totalCount == endCardDataCount;
    }
}
