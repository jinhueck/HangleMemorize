using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    public Action<int> actionForNextScene;
    public Action<int> actionForPrevScene;

    public int scenePos;

    public virtual void InitScene()
    {

    }

    public virtual void SceneStart()
    {

    }

    public virtual void SceneEnd()
    {

    }

    public virtual void GetStartInfo(int _scenePos, params object[] infoArray)
    {

    }
}
