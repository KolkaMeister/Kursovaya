using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _damage;


    public void Launch(float direction,float _force,float _damageMulti)
    {
        transform.rotation = Quaternion.Euler(0,0, direction);
        _rb.velocity = transform.right*_force;
        _damage*=_damageMulti;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
            collision.gameObject.GetComponent<ITakeDamage>()?.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
