﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Scene_002 : SceneBase
{
    public CardDataController cardDataController;
    public List<StoneData> stoneDataList = new List<StoneData>();

    [SerializeField]
    private List<CardData> totalCardDataList;

    [SerializeField] private int prevDateSecond;
    [SerializeField] private DateTime presentDateTime;

    public TimeDataController timeDataController;

    public override void InitScene()
    {
        DateTime nowTime = DateTime.Now;
        presentDateTime = nowTime;
        prevDateSecond = presentDateTime.Second + 1;
    }

    public override void SceneStart()
    {
        timeDataController.LoadCoolTimeInfo();

        if (cardDataController.CheckGameEnd())
        {
            actionForNextScene(4);
            return;
        }

        foreach (var stoneData in stoneDataList)
        {
            stoneData.cardDataList_Available.Clear();
            stoneData.cardDataList_Unavailable.Clear();
            stoneData.remainingTime = "";
            stoneData.button.onClick.RemoveAllListeners();
        }
        totalCardDataList = cardDataController.cardDataList;
        int pos = 0;
        foreach (var stoneData in stoneDataList)
        {
            var soFileData = timeDataController.scriptableObject.stoneCoolTimeInfoList[pos];
            stoneData.remainingTime = soFileData.prevJoinTime;
            stoneData.limitTime = soFileData.coolLimit;
            int presentPos = pos;
            stoneData.button.onClick.AddListener(() =>
            {
                timeDataController.ChangeCoolTimeInfo(presentPos);
                cardDataController.useCardDataList = stoneData.cardDataList_Available;
                actionForNextScene(3);
            });
            //stoneData.button.enabled = false;
            pos += 1;
        }
        for (int i = 0; i < stoneDataList.Count; i++)
        {
            int num = i;
            CreateStoneData(num);
        }
    }

    private void Update()
    {
        DateTime nowTime = DateTime.Now;
        if (nowTime.Second == 0 && prevDateSecond == 60)
        {
            prevDateSecond = nowTime.Second + 1;
        }
        else if (prevDateSecond > nowTime.Second)
        {
            presentDateTime = nowTime;
            return;
        }
        else
        {
            prevDateSecond = nowTime.Second + 1;
        }

        foreach (var stoneData in stoneDataList)
        {
            stoneData.CheckTimePassed();
        }
    }

    private bool CheckTimeGap(DateTime prevTime, DateTime nowTime)
    {
        TimeSpan gapTime = nowTime - prevTime;
        if (gapTime.Seconds > 0)
        {
            return true;
        }
        return false;
    }

    private void CreateStoneData(int pos)
    {
        List<CardData> nCardDataList = new List<CardData>(totalCardDataList.Where(x => x.cardPos == pos).ToList());
        stoneDataList[pos].cardDataList_Unavailable.AddRange(nCardDataList);
        stoneDataList[pos].CheckTimePassed();
    }

    [Serializable]
    public class StoneData
    {
        private bool bFirst = true;

        public Button button;
        public Text buttonText;
        public Text cardInfoText;

        public float limitTime;
        public string remainingTime;
        public List<CardData> cardDataList_Available = new List<CardData>();
        public List<CardData> cardDataList_Unavailable = new List<CardData>();

        private List<CardData> changeDataList = new List<CardData>();

        public void SetCardInfoText()
        {
            cardInfoText.text = "카드 갯수 : " + cardDataList_Available.Count;
        }

        public void SetButtonEnable()
        {
            DateTime nowTime = DateTime.Now;
            bool bEnableButton = true;

            if (remainingTime != null && remainingTime != "")
            {
                DateTime dateTime = DateTime.Parse(remainingTime);
                TimeSpan gapTime = nowTime - dateTime;
                int day = 0;
                int hour = 0;
                day = gapTime.Days;
                hour = day * 24 + gapTime.Hours;
                if (hour * 60 + gapTime.Minutes < limitTime)
                {
                    bEnableButton = false;
                }
            }
            if(bFirst == true)
            {
                bFirst = false;
                button.enabled = bEnableButton;
                buttonText.text = bEnableButton ? "진입 가능" : "진입 불가";
            }
            else if(button.enabled != bEnableButton)
            {
                button.enabled = bEnableButton;
                buttonText.text = bEnableButton ? "진입 가능" : "진입 불가";
            }
        }

        public void CheckTimePassed()
        {
            DateTime nowTime = DateTime.Now;
            changeDataList.Clear();
            foreach (var cardData in cardDataList_Unavailable)
            {
                string usedTime = cardData.usedTime;
                if(usedTime == null || usedTime == "")
                {
                    changeDataList.Add(cardData);
                    continue;
                }

                DateTime dateTime = DateTime.Parse(usedTime);
                TimeSpan gapTime = dateTime - nowTime;

                if(gapTime.Days > 0)
                {
                    continue;
                }
                if(gapTime.Hours > 0)
                {
                    continue;
                }
                if(gapTime.Minutes > 0)
                {
                    continue;
                }
                if (gapTime.Seconds > 0)
                {
                    continue;
                }
                changeDataList.Add(cardData);
            }

            cardDataList_Available.AddRange(changeDataList);
            cardDataList_Unavailable = cardDataList_Unavailable.Except(changeDataList).ToList();
            SetCardInfoText();
            SetButtonEnable();
        }
    }
}
