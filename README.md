# PrefabDropdown
An attribute tag to speed up finding and selecting prefabs in Unity

Just add the tag above a variable to have it display in the editor as a dropdown consisting of any prefabs that contain a component of the same type as the attributed variable
```
[PrefabDropdown]  //displays all corresponding prefabs in "Assets/Prefabs"
public Level levelToLoad;
```

You can also pass a string argument to limit the search to a specific subfolder
```
[PrefabDropdown("Levels/Subfolder")]  //displays all corresponding prefabs in "Assets/Prefabs/Levels/Subfolder"
public Level levelToLoad;
```

<a href="https://medium.com/@arcturus.studio/how-we-save-oodles-of-time-in-unity-when-working-with-prefabs-cc0bf574d979">Read the full article on Medium</a>
