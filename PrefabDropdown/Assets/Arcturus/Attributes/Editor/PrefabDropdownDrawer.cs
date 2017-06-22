
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(PrefabDropdownAttribute))]
public class PrefabDropdownDrawer : PropertyDrawer
{
    string prefabsPath = "Prefabs";

    PrefabDropdownAttribute prefabDropdown;
    List<UnityEngine.Object> prefabs;
    string[] names;
    int index = -1;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        prefabDropdown = attribute as PrefabDropdownAttribute;
        //finding all prefabs with the desired component
        if (prefabs == null)
            FindPrefabs(property);
        //finding the correct index when the component with this drawer is reshown in the inspector
        if (index == -1)
            index = Mathf.Max(0, prefabs.IndexOf((GameObject)property.objectReferenceValue));
        
        position.width -= 20;
        //showing the popup, setting the properties object refrence when index is changed
        int i = EditorGUI.Popup(position, label.text, index, names);
        if (i != index || property.objectReferenceValue==null)
        {
            index = i;
            if (prefabDropdown.propertyType == "GameObject")
                property.objectReferenceValue = ((Component)prefabs[i]).gameObject;
            else
                property.objectReferenceValue = prefabs[i];
        }

        position.x += position.width;
        position.width = 20;
        //displaying a button to allow you to quickly find the selected object in the project window
        if (GUI.Button(position, ">"))
        {
            if (property.objectReferenceValue != null)
            {
                if (prefabDropdown.propertyType == "GameObject")
                    Selection.activeGameObject = (GameObject)property.objectReferenceValue;
                else
                    Selection.activeGameObject = ((Component)property.objectReferenceValue).gameObject;
            }
        }
    }

    public void FindPrefabs(SerializedProperty property)
    {
        //finding the type of the property we are drawing
        if (prefabDropdown.propertyType == null)
            prefabDropdown.propertyType = property.type.Substring(6, property.type.Length - 7);
        
        //only scanning for gameobjects in the specified folder
        prefabs = FindPrefabsContaining(prefabsPath+prefabDropdown.addtionalPath, prefabDropdown.propertyType);
        //filling name array with the names of each prefab found for display in the dropdown
        names = new string[prefabs.Count];
        for (int i = 0; i < prefabs.Count; i++)
            names[i] = prefabs[i].name;
        
        //ensures the index is adjusted in case a prefab was added/removed and the original index no longer represents the refrenced prefab
        if (prefabDropdown.propertyType == "GameObject")
            index = Mathf.Max(0, prefabs.IndexOf(((GameObject)property.objectReferenceValue).GetComponent(prefabDropdown.propertyType)));
        else
            index = Mathf.Max(0, prefabs.IndexOf(property.objectReferenceValue));
    }

    public static List<UnityEngine.Object> FindPrefabsContaining(string path, string type)
    {
        List<UnityEngine.Object> result = new List<UnityEngine.Object>();
        //searching for all .prefab files in the specified path and subfolders
        string[] fileEntries = System.IO.Directory.GetFiles(Application.dataPath + System.IO.Path.DirectorySeparatorChar + path, "*.prefab", System.IO.SearchOption.AllDirectories);
        foreach (string fileName in fileEntries)
        {
            //getting only the part of the path that resides within the unity project
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);
            //loading the asset so we can confirm if it contains the component we are looking for
            GameObject t = (GameObject)AssetDatabase.LoadMainAssetAtPath(localPath);
            if (t == null)
                continue;
            UnityEngine.Object o = t.GetComponent(type);
            if (o != null)
                result.Add(o);
        }
        return result;
    }
    
}