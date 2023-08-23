using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class WeaponModule : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private ModificationEnum _type;
    [SerializeField] private WeaponCharacteristics _buff;
    [SerializeField] private Transform _pivotPoint;

    public string Name => _name;
    public ModificationEnum Type => _type;
    public WeaponCharacteristics Buff => _buff;
    public Transform PivotPoint => _pivotPoint;
}
