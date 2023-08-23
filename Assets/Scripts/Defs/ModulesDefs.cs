using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Defs/ModulesDefs",fileName ="ModulesDefs")]
[Serializable]
public class ModulesDefs : ScriptableObject
{

}
[Serializable]
public struct ModuleDef
{
    [SerializeField] private string name;
}

