using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonController : MonoBehaviour
{
    public string ObjectToJson<T>(T data)
    {
        return JsonUtility.ToJson(data);
    }

    public T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        string pathInfo = string.Format("{0}/{1}/{2}.json", Application.dataPath, createPath, fileName);
        FileStream fileStream = new FileStream(pathInfo, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        string pathInfo = string.Format("{0}/{1}/{2}.json", Application.dataPath, loadPath, fileName);
        if (!File.Exists(pathInfo))
        {
            return default;
        }
        FileStream fileStream = new FileStream(pathInfo, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
