using UnityEngine;
using UnityEditor;
using System.IO;

public class ScriptableObjectUtility : MonoBehaviour
{
	public string defaultPath;
	public string assetName;
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public void CreateAsset<T>(T soFileInfo) where T : ScriptableObject
	{
		T asset = soFileInfo;

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = defaultPath;
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + assetName + ".asset");

		System.IO.File.Delete(assetPathAndName);
		AssetDatabase.CreateAsset(asset, defaultPath + "/" + assetName + ".asset");

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

	public T GetAssetData<T>() where T : ScriptableObject
	{
		string assetPathAndName = defaultPath + "/" + assetName + ".asset";
		T assetData = AssetDatabase.LoadAssetAtPath(assetPathAndName, typeof(T)) as T;
		return assetData;
	}
}