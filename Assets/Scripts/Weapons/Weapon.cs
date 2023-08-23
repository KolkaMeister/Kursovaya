using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private WeaponCharacteristics _defaultCharacteristics;
    public ObservalProperty<int> _currentAmmo=new ObservalProperty<int>(0);
   // private WeaponCharacteristics _CurrentCharacteristics;
    Cooldown _fireCooldown = new Cooldown(0);

    public float ReloadTime => _defaultCharacteristics.ReloadTime;
    public bool IsEmpty => _currentAmmo.Value < 1;

    public bool IsFull => _currentAmmo.Value >= _defaultCharacteristics.MaxAmmo;
    private void Start()
    {
        _currentAmmo.Value = _defaultCharacteristics.MaxAmmo;
        _fireCooldown.SetResetTime(60f/_defaultCharacteristics.FireRate);
    }
    public void Attack()
    {
        if (!_fireCooldown.IsReady || IsEmpty)
            return;

        var bul = Instantiate<Bullet>(_bullet, _firePoint.transform.position, Quaternion.identity);
        var direction = (Vector2)(_firePoint.transform.position- _pivotPoint.transform.position).normalized;
        var degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg+ UnityEngine.Random.Range(-_defaultCharacteristics.Spread, _defaultCharacteristics.Spread);
        bul.Launch(degrees, _defaultCharacteristics.Force,_defaultCharacteristics.DamageMulti);
        _currentAmmo.Value--;
        _fireCooldown.Reset();
    }
    public void Reload()
    {
        _currentAmmo.Value = _defaultCharacteristics.MaxAmmo;
    }
}
[Serializable]
public struct WeaponCharacteristics
{
    public static WeaponCharacteristics operator +(WeaponCharacteristics first, WeaponCharacteristics second)
    {
        return new WeaponCharacteristics
        {
            _fireRate= first._fireRate + second._fireRate,
            _spread=first._spread+second._spread,
            _reloadTime= first._reloadTime + second._reloadTime,
            _damageMulti = first._damageMulti + second._damageMulti,
            _maxAmmo=first._maxAmmo+second._maxAmmo,
            _force=first._force+second._force,
        };
    }
    [SerializeField] private float _fireRate;
    [SerializeField] private float _spread;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _damageMulti;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _force;

    public float FireRate => _fireRate;
    public float Spread => _spread;
    public float ReloadTime => _reloadTime;
    public float DamageMulti => _damageMulti;
    public int MaxAmmo => _maxAmmo;

    public float Force => _force;
}

[Serializable]
public class ModificationDataModel
{
    public delegate void ModificationDataChanged(WeaponCharacteristics buffValue);
    public event ModificationDataChanged OnModificationDataChanged;
    [SerializeField] private ModificationItemData[] _data;
    public WeaponCharacteristics GetBuffValue()
    {
        WeaponCharacteristics buffValue = default;
        foreach (var item in _data)
        {
            if (item.WeaponModule == null)
                continue;
            buffValue += item.WeaponModule.Buff;
        }
        return buffValue;
    }
    public void SetModule( WeaponModule weaponModule)
        {
            
        }
}
[Serializable]
public class ModificationItemData
{
  [SerializeField]  private ModificationEnum _type;
  [SerializeField]  private Transform _pivotPoint;
  [SerializeField]  private WeaponModule _weaponModule;

    public ModificationEnum Type => _type;
    public Transform PivotPoint => _pivotPoint;
    public WeaponModule WeaponModule => _weaponModule;
}

public enum ModificationEnum
{
    barrel,
    sight,
    laser
}


