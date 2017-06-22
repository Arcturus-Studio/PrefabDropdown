using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foo : MonoBehaviour {

    [PrefabDropdown()]
    public Foo bar;

    [PrefabDropdown("Subfolder")]
    public Foo bar2;

}
