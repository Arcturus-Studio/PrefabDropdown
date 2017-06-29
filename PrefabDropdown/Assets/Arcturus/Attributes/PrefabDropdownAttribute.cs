using UnityEngine;

public class PrefabDropdownAttribute : PropertyAttribute
{
    public const string PrefabDefaultPath = "Prefabs";
    
    public string path = PrefabDefaultPath;
    public string propertyType;

    /// <summary>
    /// Use default path
    /// </summary>
    public PrefabDropdownAttribute()
    {
    }

    /// <summary>
    /// Use a subfolder of the default path
    /// </summary>
    /// <param name="subfolderPath"></param>
    public PrefabDropdownAttribute(string subfolderPath)
    {
        if(!string.IsNullOrEmpty(subfolderPath))
            path = string.Format("{0}{1}{2}", PrefabDefaultPath, System.IO.Path.DirectorySeparatorChar, subfolderPath);
    }

    /// <summary>
    /// Override defaults, define subfolders
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="subfolderPaths"></param>
    public PrefabDropdownAttribute(string rootPath, params string[] subfolderPaths)
    {   
        path = rootPath;
        for (int i = 0; i < subfolderPaths.Length; i++)
        {
            path += System.IO.Path.DirectorySeparatorChar + subfolderPaths[i];
        }
    }
}
