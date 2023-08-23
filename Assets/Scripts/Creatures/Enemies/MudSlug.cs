using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MudSlug : CreatureMob
{
    [SerializeField] private int _killBounty;
    //AttackSettings
    [SerializeField] private Transform _attackCircleCastPoint;
    [SerializeField] private Transform _attackCheckCircleCastPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _triggerAttackRadius;
    //
    readonly static int IsAttackingKey = Animator.StringToHash("IsAttacking");
    readonly static int DeathKey = Animator.StringToHash("Death");
    private Animator _animator;
    private bool _isAttacking=false;
    public bool _isEnemyClose;

    public bool IsEnemyClose=> _isEnemyClose;


    void Awake()
    {
     _animator=GetComponent<Animator>();
        OnHealthChanged += OnChangedHealth;
    }

    protected override void CalculateVelocity()
    {
        if (_isAttacking)
           _rb.velocity= new Vector2(0, _rb.velocity.y);
        else
            base.CalculateVelocity();
    }

    public override void Attack()
    {
        if (_isAttacking)
            return;
        _animator.SetBool(IsAttackingKey, true);
    }
    private  void CheackAttackTrigger()
    {
        var col = Physics2D.OverlapCircle(_attackCircleCastPoint.position, _triggerAttackRadius, LayerMask.GetMask("Player"));
        _isEnemyClose = col != null;
    }
    // AnimationEvents
    public void DealDamage()
    {
        var col= Physics2D.OverlapCircle(_attackCircleCastPoint.position, _triggerAttackRadius, LayerMask.GetMask("Player"));
        if (col == null)
            return;
        col.GetComponent<ITakeDamage>().TakeDamage(_damage);

    }
    public void OnAnimationStar()
    {
        _isAttacking = true;
    }
    public void OnAnimationaEnd()
    {
        _isAttacking = false;
        _animator.SetBool(IsAttackingKey, false);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheackAttackTrigger();
    }
    public void OnChangedHealth(float oldValue,float newValue)
    {
        if (IsDead)
        {
            _animator.SetTrigger(DeathKey);
            GetComponent<Collider2D>().enabled=false;
            FindObjectOfType<GameSession>().AddPoints(_killBounty);
            MoveDirection = Vector2.zero;
            _rb.velocity = Vector2.zero;
            _rb.Sleep();
            enabled = false;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 1, 0, 0.1f);
        Handles.DrawSolidDisc(_attackCircleCastPoint.position, Vector3.forward, _attackRadius);
        Handles.color = new Color(1, 0, 0, 0.1f);
        Handles.DrawSolidDisc(_attackCheckCircleCastPoint.position, Vector3.forward, _triggerAttackRadius);
    }


#endif



}
