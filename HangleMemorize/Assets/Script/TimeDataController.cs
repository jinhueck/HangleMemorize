using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDataController : MonoBehaviour
{
    public JsonController jsonController;
    public CoolTimeScriptableObject scriptableObject;
    public List<float> coolTimeLimitList = new List<float>();
    private List<StoneCoolTimeInfo> stoneCoolTimeInfoList;

    public string jsonFilePath;
    public string jsonFileName;

    private void Start()
    {
        LoadCoolTimeInfo();
    }

    public void LoadCoolTimeInfo()
    {
        var loadData = jsonController.LoadJsonFile<CoolTimeScriptableObject>(jsonFilePath, jsonFileName);
        if(loadData == null)
        {
            scriptableObject = new CoolTimeScriptableObject();
            stoneCoolTimeInfoList = scriptableObject.stoneCoolTimeInfoList;
            for (int i = 0; i < coolTimeLimitList.Count; i++)
            {
                StoneCoolTimeInfo stoneCoolTimeInfo = new StoneCoolTimeInfo();
                stoneCoolTimeInfo.coolLimit = coolTimeLimitList[i];
                stoneCoolTimeInfoList.Add(stoneCoolTimeInfo);
            }
        }
        else
        {
            scriptableObject = loadData;
            stoneCoolTimeInfoList = scriptableObject.stoneCoolTimeInfoList;
        }
    }

    public void ChangeCoolTimeInfo(int pos)
    {
        stoneCoolTimeInfoList[pos].prevJoinTime = DateTime.Now.ToString();
        string jsonData = jsonController.ObjectToJson<CoolTimeScriptableObject>(scriptableObject);
        jsonController.CreateJsonFile(jsonFilePath, jsonFileName, jsonData);
    }
}
