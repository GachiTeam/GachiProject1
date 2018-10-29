using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemyController : MovingObject
{
    public float lookRadius = 10f;//deprecated
    private Transform target;
    public float speed;
    public GameObject bloodEffect;

    protected override void Start_MovingObject()
    {
        target = PlayerReference.instance.player.transform;
    }
    protected override void Update_MovingObject()
    {
        float distance = target.position.x - transform.position.x;
        int sign;
        if (target.position.x < transform.position.x)
        {
            sign = -1;
        }
        else
        {
            sign = 1;
        }

        distance *= sign;

        if (distance <= lookRadius)
        {
            ComputeVelocity(sign*30, false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, lookRadius);
    }

    public void TakeDamage()
    {
        Instantiate(bloodEffect,this.transform);
    }
}
