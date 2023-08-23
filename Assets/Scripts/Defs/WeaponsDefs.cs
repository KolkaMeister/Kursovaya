using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(menuName ="Defs/WeaponsDefs",fileName ="WeaponsDefs")]
[Serializable]
public class WeaponsDefs : ScriptableObject
{

    [SerializeField] private WeaponDef[] _weaponsDefs;

    public WeaponDef[] WeaponDefs=> _weaponsDefs;
}
[Serializable]
public struct WeaponDef
{
    [SerializeField] private string _name;
    [SerializeField] private Weapon _prefab;
    [SerializeField] private Modification[] _modifications;
    [SerializeField] private WeaponCharacteristics _defaultCharacteristicks;
    [SerializeField] private int _cost;

    public string Name => _name;
    public Weapon Prefab => _prefab;
    public Modification[] Modifications => _modifications;

    public WeaponCharacteristics DefaultCharacteristicks => _defaultCharacteristicks;

    public int Cost => _cost;

}
[Serializable]
public struct Modification
{
    [SerializeField] private ModificationEnum _type;
    [SerializeField] private ModificationLevel[] _levels;

    public ModificationEnum Type => _type;
    public ModificationLevel[] Levels => _levels;
}
[Serializable]
public struct ModificationLevel
{
    [SerializeField] private int _level;
    [SerializeField] private int _cost;
    [SerializeField] private WeaponModule _module;

    public int Level => _level;
    public int Cost => _cost;
    public WeaponModule Module => _module;
}


