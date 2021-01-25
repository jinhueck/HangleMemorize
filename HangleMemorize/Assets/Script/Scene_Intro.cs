using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Scene_Intro : SceneBase
{
    public Button startButton_New;
    public Button startButton_Prev;
    public CardDataController cardDataController;

    public override void InitScene()
    {
        startButton_New.onClick.AddListener(() =>
        {
            cardDataController.SetCardData(0);
            actionForNextScene(1);
        });
        startButton_Prev.onClick.AddListener(() =>
        {
            cardDataController.SetCardData(1);
            actionForNextScene(2);
        });
        actionForPrevScene(0);
    }
}
