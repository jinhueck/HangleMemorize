using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{

    public string path;
    public string csvName;

    public CardScriptableObject GetCSVData()
    {
        CardScriptableObject cardScriptableObject = new CardScriptableObject();

        StreamReader sr = new StreamReader(Application.dataPath + "/" + path + "/" + csvName);
        bool isFirst = true;
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = sr.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split(',');
            if (isFirst == true)
            {
                isFirst = false;
            }
            else
            {
                CardData nCardData = new CardData();
                nCardData.cardName = data_values[0];
                nCardData.cardImageName = data_values[1];
                cardScriptableObject.cardDataList.Add(nCardData);
            }
        }
        return cardScriptableObject;
    }
}
