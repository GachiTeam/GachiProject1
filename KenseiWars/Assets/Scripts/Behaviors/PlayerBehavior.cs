using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;
    private Transform mTargetingTransform;
    private Transform mAnimator;

    public PhysicsBehavior b_PhysicsBehavior;
    public BasicAttackBehavior b_BasicAttackBehavior;

    //proprietati
    private bool mCanMove = true;
    //private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;
    private float mNormalSpeed;
    private float mAirSpeed;
    private float mJumpTakeOffSpeed;
    public InputManagerData mInput;
    protected Vector2 mTargetingVector;
    private bool mIsTargeting = false;
    private Sprite mNormalSprite;
    private Sprite mIsHitSprite;

    private bool mCacatBool = false;

    //Poate vrei sa restrictionezi constructorul default?
    private PlayerBehavior() { }

    //Constructorul bun

    public PlayerBehavior(GameObject _gameObject, Transform _basicAttackPrefab)
    {
        mGameObject = _gameObject;
        mTransform = mGameObject.transform;
        mBasicAttackPrefab = _basicAttackPrefab;
        mTargetingTransform = mTransform.GetChild(1);
        mAnimator = mTransform.GetChild(0);

        if(mAnimator == null)
        {
            Debug.LogWarning("player Animator is null!");
        }

        //mMaxSpeed = PlayerReference.instance.playerMaxSpeed;
        mNormalSpeed = GlobalValues.instance.playerNormalSpeed;
        mAirSpeed = GlobalValues.instance.playerAirSpeed;
        //mJumpTakeOffSpeed = PlayerReference.instance.playerMaxJump;
        mJumpTakeOffSpeed = GlobalValues.instance.jumpForce;

        b_PhysicsBehavior = new PhysicsBehavior(mGameObject, mGameObject.GetComponent<Rigidbody2D>());
        b_BasicAttackBehavior = new BasicAttackBehavior(mBasicAttackPrefab);
        b_BasicAttackBehavior.AddHitableTag("enemy");

        mBehaviorsList.Add(b_PhysicsBehavior);
        mBehaviorsList.Add(b_BasicAttackBehavior);

        mFacingDirection = DIRECTION.RIGHT;
        mTargetingVector = Vector2.right;

        mNormalSprite = mGameObject.GetComponent<SpriteRenderer>().sprite;
        mIsHitSprite = GlobalSpriteReference.instance.EnemyHit;
    }

    protected override void UpdateMyBehavior()
    {
    }

    protected override void FixedUpdateMyBehavior()
    {
        Jump();
        MeleAttack();
        FacingDirection();
        TargetingVector();
        UpdateGlobalValues();
        Move();
    }

    void TargetingVector()
    {
        if (Input.GetAxis(mInput.triggers) < 0 && b_PhysicsBehavior.GetCanMove() == true)
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
        if (Input.GetAxis(mInput.x) != 0)
        {
            Vector2 position = mTransform.position;
            position.x += 2.1f * (mFacingDirection == DIRECTION.RIGHT ? 1 : -1);
            b_BasicAttackBehavior.Attack(position, 0.1f);

            MeleAttackAnimation();
        }
    }

    void MeleAttackAnimation()
    {

    }

    void Move()
    {
        float inputValue;

        inputValue = Input.GetAxis(mInput.joystickHorizontal);

        if (mIsTargeting == false && inputValue != 0)
        {
            mCanMove = true;
        }
        else
        {
            mCanMove = false;
        }

        if (mCanMove == true)
        {
            float speed;

            if(b_PhysicsBehavior.GetIsGrounded() == true)
            {
                speed = mNormalSpeed;
            }
            else
            {
                speed = mAirSpeed;
            }

            if (inputValue > 0)
            {
                b_PhysicsBehavior.SetMoving(DIRECTION.RIGHT, speed);
            }
            else
            {
                b_PhysicsBehavior.SetMoving(DIRECTION.LEFT, speed);
            }
        }
        else
        {
            b_PhysicsBehavior.SetMoving(DIRECTION.DEFAULT, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown(mInput.a) && b_PhysicsBehavior.GetCanMove() == true)
        {
            b_PhysicsBehavior.Jump(mJumpTakeOffSpeed);
        }
    }

    void FacingDirection()
    {
        if (b_PhysicsBehavior.GetVelocity().x > 0)
        {
            if (mFacingDirection == DIRECTION.LEFT)
                mCacatBool = true;
                mFacingDirection = DIRECTION.RIGHT;
            mTargetingVector = Vector2.right;
            b_BasicAttackBehavior.SetIsFacingRight(true);
        }
        if (b_PhysicsBehavior.GetVelocity().x < 0)
        {
            if (mFacingDirection == DIRECTION.RIGHT)
                mCacatBool = true;
            mFacingDirection = DIRECTION.LEFT;
            mTargetingVector = Vector2.left;
            b_BasicAttackBehavior.SetIsFacingRight(false);
        }
        if(mCacatBool==true)
        {

            if (mGameObject.GetComponent<SpriteRenderer>().flipX == true)
                mGameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                mGameObject.GetComponent<SpriteRenderer>().flipX = true;
            mCacatBool = false;
        }
    }

    public void IsHit()
    {
        mGameObject.GetComponent<GenericActor>().StartCoroutine(IsHitCoroutine());//hacks dar are sens
    }

    public IEnumerator IsHitCoroutine()
    {
        /*
        mHP--;
        if (mHP < 0)
        {
            Object.Destroy(mGameObject);
        }
        */

        b_BasicAttackBehavior.SetMeleAttackPassedTime(0);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mIsHitSprite;

        yield return new WaitForSeconds(0.05f);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mNormalSprite;
    }

    public void SetInput(int _joystickID)
    {
        mInput = new InputManagerData(_joystickID);
    }

    public void SetCanMeleAttack(bool _canMeleAttack)
    {
        b_BasicAttackBehavior.SetCanMeleAttack(_canMeleAttack);
    }

    public bool IsFacingDirectionRight()
    {
        return mFacingDirection == DIRECTION.RIGHT;
    }

    public void SetMeleAttackTime(float _meleAttackTime)
    {
        b_BasicAttackBehavior.SetMeleAttackTime(_meleAttackTime);
    }

    public void SetCanMove(bool _canMove)
    {
        b_PhysicsBehavior.SetCanMove(_canMove);
    }

    public bool GetIsTargeting()
    {
        return mIsTargeting;
    }

    public Vector2 GetTargetingVector()
    {
        return mTargetingVector;
    }

    void UpdateGlobalValues()
    {
        mNormalSpeed = GlobalValues.instance.playerNormalSpeed;
        mAirSpeed = GlobalValues.instance.playerAirSpeed;
        mJumpTakeOffSpeed = GlobalValues.instance.jumpForce;
    }
}
