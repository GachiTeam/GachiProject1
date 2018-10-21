using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : GenericActor
{
    //referinte la componente ale obiectului
    public Transform mShootingPrefab;
    public Transform mBasicAttackPrefab;
    public Transform mOilPotPrefab;

    private SpriteRenderer mSwordSpriteReneder;
    private PhysicsBehavior b_PhysicsBehavior;
    private ShootingBehavior b_ShootingBehavior;
    private OilPotBehavior b_OilPotBehavior; 
    private BasicAttackBehavior b_BasicAttackBehavior;

    //proprietati
    private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;
    public float mMaxSpeed = 3;
    public float mJumpTakeOffSpeed = 7;

    //sword animation
    private float mSwordRotatingTime = 0.1f;
    private float mSwordTimeSpent = 0;
    private int mSwordState = 0;


    protected override void StartActor()
    {
        b_PhysicsBehavior = new PhysicsBehavior(gameObject, GetComponent<Rigidbody2D>());
        b_ShootingBehavior = new ShootingBehavior(mShootingPrefab);
        b_BasicAttackBehavior = new BasicAttackBehavior(mBasicAttackPrefab);
        b_OilPotBehavior = new OilPotBehavior(mOilPotPrefab);

        mBehaviorsList.Add(b_PhysicsBehavior);
        mBehaviorsList.Add(b_ShootingBehavior);
        mBehaviorsList.Add(b_BasicAttackBehavior);
        mBehaviorsList.Add(b_OilPotBehavior);

        mFacingDirection = DIRECTION.RIGHT;
    }

    protected override void UpdateActor()
    {
        Move();
        Jump();
        //Shoot();
        Attack();
        OilPot();
        FacingDirection();
    }

    void OilPot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            b_OilPotBehavior.Shoot(new Vector2(transform.position.x+10, transform.position.y), direction, 0.1f);
        }
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            b_ShootingBehavior.Shoot(transform.position, direction, 0.1f, 3.0f);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 position = transform.position;
            position.x += 2.1f * (mFacingDirection == DIRECTION.RIGHT ? 1 : -1);
            b_BasicAttackBehavior.Attack(position, 0.01f);

            //for sword animation
            mSwordState = 1;
        }

        SwordAnimation();
    }

    void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        b_PhysicsBehavior.SetMoving(direction, mMaxSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            b_PhysicsBehavior.Jump(mJumpTakeOffSpeed);
        }
    }

    void FacingDirection()
    {
        if(b_PhysicsBehavior.GetVelocity().x > 0)
        {
            mFacingDirection = DIRECTION.RIGHT;
        }
        if(b_PhysicsBehavior.GetVelocity().x < 0)
        {
            mFacingDirection = DIRECTION.LEFT;
        }

        Transform sword = transform.GetChild(0);
        Transform swordSpriteTransform = sword.GetChild(0);

        SpriteRenderer mSwordSpriteReneder = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        mSwordSpriteReneder.flipX = mFacingDirection == DIRECTION.LEFT ? true : false;

        if(swordSpriteTransform.localPosition.x < 0 && mFacingDirection == DIRECTION.RIGHT)
        {
            swordSpriteTransform.localPosition = new Vector3(swordSpriteTransform.localPosition.x * (-1), swordSpriteTransform.localPosition.y, -0.1f);
        }
        else if (swordSpriteTransform.localPosition.x > 0 && mFacingDirection == DIRECTION.LEFT)
        {
            swordSpriteTransform.localPosition = new Vector3(swordSpriteTransform.localPosition.x * (-1), swordSpriteTransform.localPosition.y, -0.1f);
        }
    }

    void SwordAnimation()
    {
        if(mSwordState == 1)
        {
            mSwordTimeSpent = mSwordTimeSpent + Time.deltaTime;

            if (mFacingDirection == DIRECTION.RIGHT)
            {
                transform.GetChild(0).Rotate(Vector3.back, 800 * Time.deltaTime);
            }
            else
            {
                transform.GetChild(0).Rotate(Vector3.forward, 800 * Time.deltaTime);
            }

            if(mSwordTimeSpent > mSwordRotatingTime)
            {
                mSwordTimeSpent = 0;
                mSwordState = 2;
            }
        }
        else if(mSwordState == 2)
        {
            mSwordTimeSpent = mSwordTimeSpent + Time.deltaTime;

            if (mFacingDirection == DIRECTION.RIGHT)
            {
                transform.GetChild(0).Rotate(Vector3.forward, 800 * Time.deltaTime);
            }
            else
            {
                transform.GetChild(0).Rotate(Vector3.back, 800 * Time.deltaTime);
            }

            if (mSwordTimeSpent > mSwordRotatingTime)
            {
                mSwordTimeSpent = 0;
                mSwordState = 0;
                transform.GetChild(0).rotation = Quaternion.identity;           }
        }
    }
}
