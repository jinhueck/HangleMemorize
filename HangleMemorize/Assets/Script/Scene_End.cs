using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Playables;


public class Scene_End : SceneBase
{
    public PlayableDirector playableDirector;
    public Button skipButton;

    [SerializeField] private float playTime = 0f;
    [SerializeField] private float presentTime = 0f;

    public CardDataController cardDataController;
    public TimeDataController timeDataController;

    public override void InitScene()
    {
        playTime = (float)playableDirector.duration;
    }

    public override void SceneStart()
    {
        presentTime = 0f;
        playableDirector.Play();
    }

    public override void SceneEnd()
    {
        cardDataController.DeleteJsonFile();
        timeDataController.DeleteJsonFile();
        actionForNextScene(0);
    }

    private void Update()
    {
        if (presentTime >= playTime)
        {
            SceneEnd();
            return;
        }
        presentTime += Time.deltaTime;
    }
}
