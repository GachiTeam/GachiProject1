using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    protected Animator animator;
    private Rigidbody2D rb;

    int contor = 0;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start_MovingObject()
    { }

    protected virtual void Update_MovingObject()
    { }

    protected virtual void FixedUpdate_MovingObject()
    { }
    protected override void Update_PhysicsObject()
    {
        Update_MovingObject();

        animator.SetBool("Ground", true);// grounded);
        animator.SetFloat("vSpeed", (float)rb.velocity.y);
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", (float)Mathf.Abs(moveHorizontal));
    }

    protected override void Start_PhysicsObject()
    {
        Start_MovingObject();
    }


    protected override void FixedUpdate_PhysicsObject()
    {
        FixedUpdate_MovingObject();
    }


    protected override void ComputeVelocity(float speed, bool isPlayer)
    {
        Vector2 move = Vector2.zero;

        move.x = speed;

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    protected void SetVelocity_Y(float input)
    {
        velocity.y =input;
    }


}