using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureStorage : MonoBehaviour
{
    public string texturePath;
    public Dictionary<string, Sprite> cardImageDic = new Dictionary<string, Sprite>();

    private void Start()
    {
        SetTextureList();
    }

    public void SetTextureList()
    {
        var resourceArray = Resources.LoadAll<Sprite>(texturePath);
        foreach (var sprite in resourceArray)
        {
            string key = sprite.name.ToString() + ".png";
            cardImageDic.Add(key, sprite);
        }
    }

    public Sprite GetSpriteInfo(string key)
    {
        Sprite returnValue = null;
        if(cardImageDic.ContainsKey(key))
        {
            returnValue = cardImageDic[key];
        }
        return returnValue;
    }
}
