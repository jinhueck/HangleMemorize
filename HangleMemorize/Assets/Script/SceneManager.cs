using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public List<SceneBase> sceneBaseList = new List<SceneBase>();
    public int startScenePos;
    private void Start()
    {
        for (int i = 0; i < sceneBaseList.Count; i++)
        {
            SceneBase sceneBase = sceneBaseList[i];
            sceneBase.actionForNextScene += GoToNextScene;
            sceneBase.actionForPrevScene += GoToPrevScene;
            sceneBase.InitScene();
        }
        sceneBaseList[startScenePos].SceneStart();
    }

    private void GoToNextScene(int pos)
    {
        for (int i = 0; i < sceneBaseList.Count; i++)
        {
            int num = i;
            bool bActive = pos == num;
            var sceneBase = sceneBaseList[num];
            sceneBase.gameObject.SetActive(bActive);
        }
        sceneBaseList[pos].SceneStart();
    }

    private void GoToPrevScene(int pos)
    {
        for (int i = 0; i < sceneBaseList.Count; i++)
        {
            int num = i;
            bool bActive = pos == num;
            var sceneBase = sceneBaseList[num];
            sceneBase.gameObject.SetActive(bActive);
        }
        sceneBaseList[pos].SceneStart();
    }
}
