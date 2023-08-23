using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Defs/DefsFacade",fileName ="DefsFacade")]
public class DefsFacade : ScriptableObject
{
    public static DefsFacade _instance;

    public static DefsFacade Instance => _instance == null ? Load() : _instance;

    public static DefsFacade Load()
    {
        return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }

    [SerializeField] private WeaponsDefs _weaponsDefs;
    [SerializeField] private ModulesDefs _modulesDefs;

    public WeaponsDefs WeaponsDefs => _weaponsDefs;
}
