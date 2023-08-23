using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : Creature
{
    [SerializeField] private Transform elbowJoint;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _wheel;
    [SerializeField] private GameObject _head;
    [SerializeField] private float _rotationSpeed;
    public Weapon Weapon => _weapon;

    private Coroutine _reloadRoutine;

    private void Start()
    {
        OnHealthChanged += HealthChanged;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CalcHandRotation();
        RotateHead();
        CalcWheelRotate();
    }
    private void CalcWheelRotate()
    {
        // var newRotationZ = _wheel.transform.localRotation.z + _rotationSpeed * Time.deltaTime * MoveDirection.x;
        //_wheel.transform.localRotation= Quaternion.Euler(_wheel.transform.localRotation.x, _wheel.transform.localRotation.y, newRotationZ);
        _wheel.transform.Rotate(0, 0, -360f * Time.deltaTime*MoveDirection.x*transform.localScale.x, Space.Self);
    }
    public void Interact()
    {
        Collider2D[] res= new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc(transform.position, 1f, res,LayerMask.GetMask("Interactable"));
       // if (res[0]!=null)
            

    }
    private void RotateHead()
    {
        var vec = TargetPos - (Vector2)_head.transform.position;
        var rotateDeg = Mathf.Rad2Deg * Mathf.Atan(vec.y / vec.x);
        _head.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(rotateDeg,-45f,30));
    }
    private void CalcHandRotation()
    {
        var vec = TargetPos - (Vector2)elbowJoint.position;
        var rotateDeg = Mathf.Rad2Deg*Mathf.Atan(vec.y/vec.x);
        elbowJoint.transform.rotation = Quaternion.Euler(0,0, rotateDeg);
    }

    public override void Attack()
    {
        if (_reloadRoutine != null)
            return;
        if (_weapon.IsEmpty)
        {
            Reload();
            return;
        }
        _weapon.Attack();
    }
    public void Reload()
    {
        if (_reloadRoutine == null&&!_weapon.IsFull)
            _reloadRoutine= StartCoroutine(ReloadRoutine());
    }
    public IEnumerator ReloadRoutine()
    {
        var endTime = Time.time+_weapon.ReloadTime;
        while(Time.time<endTime)
        {
            yield return null;
        }
        _weapon.Reload();
        var r= _reloadRoutine;
        _reloadRoutine = null;
        StopCoroutine(r);
    }
    public void HealthChanged(float oldValue,float newValue)
    {
        if (IsDead)
            LevelLoadManager.loadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
