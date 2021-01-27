using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Scene_003 : SceneBase
{
    public CardDataController cardDataController;
    public List<CardData> cardDataList;
    public List<CardData> successCardList = new List<CardData>();
    public List<CardData> failCardList = new List<CardData>();
    public int cardPos;
    private CardData currentCardData;

    public Button successButton;
    public Button failButton;
    public Button TurnButton;

    public Text InitialText;
    public Text originText;

    public override void InitScene()
    {
        successButton.onClick.AddListener(() =>
        {
            CheckSuccessCard(true);
        });

        failButton.onClick.AddListener(() =>
        {
            CheckSuccessCard(false);
        });
    }

    public override void SceneStart()
    {
        cardPos = 0;
        cardDataList = cardDataController.useCardDataList;
        successCardList.Clear();
        failCardList.Clear();
        SetCurrentCardData();
    }

    private void SetCardData()
    {
        originText.text = currentCardData.cardName;
        InitialText.text = currentCardData.cardInitialName;
    }

    private void SaveCardInfo()
    {
        string nowTime =  DateTime.Now.ToString();
        for (int i = 0; i < successCardList.Count; i++)
        {
            CardData cardData = successCardList[i];
            cardData.cardPos += 1;
            if (cardData.cardPos > 5)
                cardData.cardPos = 5;
            cardData.usedTime = nowTime;
        }
        for (int i = 0; i < failCardList.Count; i++)
        {
            CardData cardData = failCardList[i];
            cardData.cardPos -= 1;
            if (cardData.cardPos < 0)
                cardData.cardPos = 0;
            cardData.usedTime = nowTime;
        }
        cardDataController.SaveCardData();
    }

    private void SetCurrentCardData()
    {
        int presentPos = cardPos;
        cardPos += 1;
        if(cardDataList.Count < cardPos)
        {
            SaveCardInfo();
            cardDataController.SetCardData(1);
            actionForNextScene(2);
            return;
        }
        currentCardData = cardDataList[presentPos];
        SetCardData();
    }

    private void CheckSuccessCard(bool bSuccess)
    {
        if(bSuccess)
        {
            successCardList.Add(currentCardData);
        }
        else
        {
            failCardList.Add(currentCardData);
        }
        SetCurrentCardData();
    }
}
