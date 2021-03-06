﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Playables;

public class Scene_001 : SceneBase
{
    public PlayableDirector playableDirector;
    public Button skipButton;

    [SerializeField] private float playTime = 0f;
    [SerializeField] private float presentTime = 0f;

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
        actionForNextScene(2);
    }

    private void Update()
    {
        if(presentTime >= playTime)
        {
            SceneEnd();
            return;
        }
        presentTime += Time.deltaTime;
    }
}
