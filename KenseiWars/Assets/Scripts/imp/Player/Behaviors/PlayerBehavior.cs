using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;

    private SpriteRenderer mSwordSpriteReneder;
    private PhysicsBehavior b_PhysicsBehavior;
    private BasicAttackBehavior b_BasicAttackBehavior;

    //proprietati
    private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;
    private float mMaxSpeed;
    private float mJumpTakeOffSpeed;
    public InputManagerData mInput;
    protected Vector2 mTargetingVector;
    public bool mIsTargeting = false;

    //mele attack shit. FOARTE IMPORTANT!!! MODULARIZEAZA ATACUL!!!!
    private bool mCanMeleAttack = true;
    private bool mIsMeleAttackOnCooldown = false;
    private float mMeleAttackPassedTime = 0;
    private float mMeleAttackTime = 0.25f;

    //sword animation
    private float mSwordRotatingTime = 0.1f;
    private float mSwordTimeSpent = 0;
    private int mSwordState = 0;

    //Poate vrei sa restrictionezi constructorul default?
    private PlayerBehavior() { }

    //Constructorul bun

    public PlayerBehavior(GameObject gameObject, Transform basicAttackPrefab)
    {
        mGameObject = gameObject;
        mTransform = mGameObject.transform;
        mBasicAttackPrefab = basicAttackPrefab;

        mMaxSpeed = PlayerReference.instance.playerMaxSpeed;
        mJumpTakeOffSpeed = PlayerReference.instance.playerMaxJump;
    }

    protected override void StartMyBehavior()
    {
        b_PhysicsBehavior = new PhysicsBehavior(mGameObject, mGameObject.GetComponent<Rigidbody2D>());
        b_BasicAttackBehavior = new BasicAttackBehavior(mBasicAttackPrefab);

        mBehaviorsList.Add(b_PhysicsBehavior);
        mBehaviorsList.Add(b_BasicAttackBehavior);

        mFacingDirection = DIRECTION.RIGHT;
        mTargetingVector = Vector2.right;
    }

    protected override void UpdateMyBehavior()
    {
        Move();
        Jump();
        MeleAttack();
        FacingDirection();
        TargetingVector();
        Cooldown();
    }

    void TargetingVector()
    {
        if (Input.GetAxis(mInput.triggers) < 0)
        {
            mIsTargeting = true;
            Vector2 rawDirection = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            mTargetingVector = rawDirection.normalized;

            mTransform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            Transform targetingTransform = mTransform.GetChild(1);

            float zAxis = Vector2.Angle(Vector2.right, mTargetingVector) * (Input.GetAxis(mInput.joystickVertical) > 0 ? -1 : 1);

            targetingTransform.eulerAngles = new Vector3(0, 0, zAxis);

            //flag infect, bad practice, cere refactor
            if(Input.GetAxis(mInput.joystickHorizontal) == 0 && Input.GetAxis(mInput.joystickVertical) == 0 && mFacingDirection == DIRECTION.LEFT)
            {
                targetingTransform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
        else
        {
            mIsTargeting = false;

            mTransform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void MeleAttack()
    {
        if (mCanMeleAttack == true && mIsMeleAttackOnCooldown == false)
        {
            if (Input.GetAxis(mInput.x) != 0)
            {
                Vector2 position = mTransform.position;
                position.x += 2.1f * (mFacingDirection == DIRECTION.RIGHT ? 1 : -1);
                b_BasicAttackBehavior.Attack(position, 0.01f);

                //for sword animation
                mSwordState = 1;
            }

            SwordAnimation();
            mIsMeleAttackOnCooldown = true;
        }
    }

    void Move()
    {
        float direction;

        if (mIsTargeting == false)
        {
            direction = Input.GetAxis(mInput.joystickHorizontal);
        }
        else
        {
            direction = 0;
        }

        b_PhysicsBehavior.SetMoving(direction, mMaxSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown(mInput.a))
        {
            b_PhysicsBehavior.Jump(mJumpTakeOffSpeed);
        }
    }

    void FacingDirection()
    {
        if (b_PhysicsBehavior.GetVelocity().x > 0)
        {
            mFacingDirection = DIRECTION.RIGHT;
            mTargetingVector = Vector2.right;
        }
        if (b_PhysicsBehavior.GetVelocity().x < 0)
        {
            mFacingDirection = DIRECTION.LEFT;
            mTargetingVector = Vector2.left;
        }

        Transform sword = mTransform.GetChild(0);
        Transform swordSpriteTransform = sword.GetChild(0);

        SpriteRenderer mSwordSpriteReneder = mTransform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        mSwordSpriteReneder.flipX = mFacingDirection == DIRECTION.LEFT ? true : false;

        if (swordSpriteTransform.localPosition.x < 0 && mFacingDirection == DIRECTION.RIGHT)
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
        if (mSwordState == 1)
        {
            mSwordTimeSpent = mSwordTimeSpent + Time.deltaTime;

            if (mFacingDirection == DIRECTION.RIGHT)
            {
                mTransform.GetChild(0).Rotate(Vector3.back, 800 * Time.deltaTime);
            }
            else
            {
                mTransform.GetChild(0).Rotate(Vector3.forward, 800 * Time.deltaTime);
            }

            if (mSwordTimeSpent > mSwordRotatingTime)
            {
                mSwordTimeSpent = 0;
                mSwordState = 2;
            }
        }
        else if (mSwordState == 2)
        {
            mSwordTimeSpent = mSwordTimeSpent + Time.deltaTime;

            if (mFacingDirection == DIRECTION.RIGHT)
            {
                mTransform.GetChild(0).Rotate(Vector3.forward, 800 * Time.deltaTime);
            }
            else
            {
                mTransform.GetChild(0).Rotate(Vector3.back, 800 * Time.deltaTime);
            }

            if (mSwordTimeSpent > mSwordRotatingTime)
            {
                mSwordTimeSpent = 0;
                mSwordState = 0;
                mTransform.GetChild(0).rotation = Quaternion.identity;
            }
        }
    }

    public void SetInput(int _joystickID)
    {
        mInput = new InputManagerData(_joystickID);
    }

    void Cooldown()
    {
        if(mIsMeleAttackOnCooldown == true)
        {
            mMeleAttackPassedTime += Time.deltaTime;
            if(mMeleAttackPassedTime>mMeleAttackTime)
            {
                mMeleAttackPassedTime = 0;
                mIsMeleAttackOnCooldown = false;
            }
        }
    }

    public void SetCanMeleAttack(bool _canMeleAttack)
    {
        mCanMeleAttack = _canMeleAttack;
    }

    public bool IsFacingDirectionRight()
    {
        return mFacingDirection == DIRECTION.RIGHT;
    }
}
