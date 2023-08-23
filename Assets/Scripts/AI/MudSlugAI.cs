using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlugAI : MonoBehaviour
{
    private MudSlug _mudSlug;

    private Player _player;

    private void Start()
    {
        _mudSlug = GetComponent<MudSlug>();
        _player=FindObjectOfType<Player>();
        _mudSlug.OnHealthChanged += OnHealthChanged;
    }

    private void Update()
    {
        if (_player == null)
            return;
        _mudSlug.MoveDirection =(_player.transform.position- transform.position).normalized;
        _mudSlug.TargetPos = _player.transform.position;
        if (_mudSlug.IsEnemyClose)
            _mudSlug.Attack();
    }

    public void OnHealthChanged(float oldValue, float newValue)
    {
        if (_mudSlug.IsDead)
        {
            Destroy(gameObject,10f);
            enabled = false;
        }
    }
}
