using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface ITakeDamage
{
    public delegate void HealthChanged(float oldValue, float newValue);
    public event HealthChanged OnHealthChanged;
    public bool IsDead { get;}
    public float Health { get;}
    public void TakeDamage(float damage);
    public void TakeHeal(float heal);
}
