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

    public TextureStorage textureStorage;

    [SerializeField]
    public CardInfo[] cardInfoArray = new CardInfo[2];

    public override void InitScene()
    {
        foreach (var cardInfo in cardInfoArray)
        {
            cardInfo.actionForCheckSuccessCard += CheckSuccessCard;
            cardInfo.InitClassData();
        }
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
        bool bOddNumber = (cardPos - 1) % 2 == 1;
        int selectPos = bOddNumber ? 1 : 0;
        cardInfoArray[selectPos].SetCardData(textureStorage, currentCardData);
        for (int i = 0; i < cardInfoArray.Length; i++)
        {
            cardInfoArray[i].SetButtonEnable(selectPos == i);
        }
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
        if (cardDataList.Count < cardPos)
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

[Serializable]
public class CardInfo
{
    public AnimationController animationController;
    public Action<bool> actionForCheckSuccessCard;
    public Button successButton;
    public Button failButton;
    public Button[] turnButtonArray;

    public Image cardImage;
    public Text InitialText;
    public Text originText;

    private bool bFront = true;
    //public Animator animatorCardTurn;
    public string animCardFront;
    public string animCardBack;

    public void InitClassData()
    {
        successButton.onClick.AddListener(() =>
        {
            actionForCheckSuccessCard?.Invoke(true);
        });

        failButton.onClick.AddListener(() =>
        {
            actionForCheckSuccessCard?.Invoke(false);
        });
        foreach (var turnButton in turnButtonArray)
        {
            turnButton.onClick.AddListener(() =>
            {
                animationController.PlayAnimation(bFront ? AnimationState.Anim_TurnBack : AnimationState.Anim_TurnFront);
                //animatorCardTurn.Play(bFront ? animCardBack : animCardFront, 0, 0);
                bFront = !bFront;
            });
        }
    }

    public void SetCardData(TextureStorage textureStorage, CardData currentCardData)
    {
        cardImage.sprite = textureStorage.GetSpriteInfo(currentCardData.cardImageName);
        cardImage.SetNativeSize();
        originText.text = currentCardData.cardName;
        InitialText.text = currentCardData.cardInitialName;
    }

    public void SetButtonEnable(bool bEnable)
    {
        successButton.enabled = bEnable;
        failButton.enabled = bEnable;
        foreach (var turnButton in turnButtonArray)
        {
            turnButton.enabled = bEnable;
        }
    }
}