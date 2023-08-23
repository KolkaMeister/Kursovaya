using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataInterface : MonoBehaviour
{
    [SerializeField] private Text _points;
    [SerializeField] private Text _ammo;
    [SerializeField] private Image _healthBar;
    [SerializeField] private int _scoreReaction;
    private float _maxHealth;

    static readonly int IsUpgradedKey = Animator.StringToHash("IsUpgraded");
    private Animator _animator;
    private void Start()
    {
        _animator= GetComponent<Animator>();
        var player = FindObjectOfType<Player>();
        _maxHealth = player.Health;
        player.OnHealthChanged += HealthBarUpdate;
        player.Weapon._currentAmmo.OnChanged += AmmoUpdate;
        var session =FindObjectOfType<GameSession>();
        var o= FindObjectsOfType<GameSession>();
        session.Points.OnChanged += PointsUpdate;
        AmmoUpdate(0,player.Weapon._currentAmmo.Value);
        HealthBarUpdate(0, player.Health);
        PointsUpdate(0, session.Points.Value);

    }
    public void HealthBarUpdate(float oldValue,float newValue)
    {
        _healthBar.fillAmount = newValue/_maxHealth;
    }
    public void AmmoUpdate(int oldValue,int newValue)
    {
        _ammo.text= newValue.ToString();
    }
    public void PointsUpdate(int oldValue,int newValue)
    {
        _points.text = newValue.ToString();
        _animator.SetBool(IsUpgradedKey, newValue>_scoreReaction);
    }
}
