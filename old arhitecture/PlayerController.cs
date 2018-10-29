using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingObject
{

    //for attack
    public float startTimeBtwAttack;
    public float attackRange;
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    private Hook hook;
    //private Animator animator;

    private float timeBtwAttack;
    private SpriteRenderer spriteRenderer;

    protected override void Update_MovingObject()
    {
        //Debug.Log(timeBtwAttack+" ");
        /*
        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            hook.Grab(this, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/

        Move();

        Jump();

        Attack();
    }

    protected override void Start_MovingObject()
    {
        animator = GetComponent<Animator>();
        hook = new Hook();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void FixedUpdate_MovingObject()
    {
    }

    void Attack()
    {
        if (timeBtwAttack <= 0)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; ++i)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage();
                    Debug.Log(i);
                }
                timeBtwAttack = startTimeBtwAttack;

                animator.SetTrigger("playerAttack");
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            SetVelocity_Y(jumpTakeOffSpeed);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                SetVelocity_Y(velocity.y * 0.5f);
            }
        }
    }

    void Move()
    {
        float speed = Input.GetAxis("Horizontal");
        bool flipSprite = (spriteRenderer.flipX ? (speed > 0.01f) : (speed < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        ComputeVelocity(speed, true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
