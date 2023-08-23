using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservalProperty<TType>
{
    private TType _value;

    public delegate void OnChangedValueChanged(TType oldValue, TType newValue);
    public event OnChangedValueChanged OnChanged;
    public  ObservalProperty(TType value)
    {
        _value = value;
    }

    public TType Value
    {
        get { return _value; }

        set 
        {
            if (_value.Equals(value))
                return;
            var old = _value;
            _value = value;
            OnChanged?.Invoke(old, value);
        }
    }
    
}
