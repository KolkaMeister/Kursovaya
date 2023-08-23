using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    private GameObject target;

    private void Awake()
    {
        target = FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        Follow();
    }
    private void Follow()
    {
        if (target == null)
            return;
        transform.position = new Vector3( target.transform.position.x,target.transform.position.y+1.2f,transform.position.z);
    }
}
