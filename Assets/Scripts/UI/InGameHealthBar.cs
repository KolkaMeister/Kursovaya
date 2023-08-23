using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHealthBar : MonoBehaviour
{
    [SerializeField] private Image _img;
    private float _maxValue;
    private void Start()
    {
       var player = FindObjectOfType<Player>();
        _maxValue = player.Health;
        player.OnHealthChanged += HealthBarUpdate;
        HealthBarUpdate(0, player.Health);
    }

    public void HealthBarUpdate(float oldValue, float newValue)
    {
        _img.fillAmount = newValue/_maxValue;
    }

}
