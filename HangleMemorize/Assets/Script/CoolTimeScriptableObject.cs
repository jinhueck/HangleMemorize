using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CoolTimeScriptableObject
{
    public List<StoneCoolTimeInfo> stoneCoolTimeInfoList = new List<StoneCoolTimeInfo>();
}
[Serializable]
public class StoneCoolTimeInfo
{
    public float coolLimit;
    public string prevJoinTime;
}