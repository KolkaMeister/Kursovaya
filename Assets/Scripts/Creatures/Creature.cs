using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour, ITakeDamage
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _MaxHealth;
    private Vector2 moveDirection;
    private Vector2 targetPos;

    public event ITakeDamage.HealthChanged OnHealthChanged;
    [SerializeField] private float _health;

    public float Health { get => _health; }
    public bool IsDead { get => !(_health > 0); }
    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _health = _MaxHealth;
    }
    public Vector2 MoveDirection
    {
        get { return moveDirection; } 
        set {moveDirection = value; }
    }
    public Vector2 TargetPos
    {
        get { return targetPos; }
        set{ targetPos = value; }
    }
    protected virtual void CalculateVelocity()
    {
        _rb.velocity = new Vector2(MoveDirection.x * _moveSpeed, _rb.velocity.y);
    }

    protected virtual void CalcBodyRotation()
    {
        var vec = targetPos.x - transform.position.x;
        if (vec>0)
            transform.localScale= new Vector3(1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }
    protected virtual void FixedUpdate()
    {
        CalcBodyRotation();
        CalculateVelocity();
    }
    public virtual void Attack()
    {
    }
    public virtual void TakeDamage(float damage)
    {
        if (!IsDead)
        {
        var old = _health;
        _health -= damage;
        OnHealthChanged?.Invoke(old, _health);
        }
    }

    public virtual void TakeHeal(float heal)
    {
        
    }
}
