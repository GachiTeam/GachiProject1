using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;

    public PhysicsBehavior b_PhysicsBehavior;
    public BasicAttackBehavior b_BasicAttackBehavior;

    //proprietati
    private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;
    private float mMaxSpeed;
    private float mJumpTakeOffSpeed;
    public InputManagerData mInput;
    protected Vector2 mTargetingVector;
    public bool mIsTargeting = false;

    //Poate vrei sa restrictionezi constructorul default?
    private PlayerBehavior() { }

    //Constructorul bun

    public PlayerBehavior(GameObject _gameObject, Transform _basicAttackPrefab)
    {
        mGameObject = _gameObject;
        mTransform = mGameObject.transform;
        mBasicAttackPrefab = _basicAttackPrefab;

        mMaxSpeed = PlayerReference.instance.playerMaxSpeed;
        mJumpTakeOffSpeed = PlayerReference.instance.playerMaxJump;

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
    }

    void TargetingVector()
    {
        if (Input.GetAxis(mInput.triggers) < 0)
        {
            mIsTargeting = true;
            Vector2 rawDirection = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            mTargetingVector = rawDirection.normalized;

            mTransform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            Transform targetingTransform = mTransform.GetChild(0);

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

            mTransform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void MeleAttack()
    {
        if (Input.GetAxis(mInput.x) != 0)
        {
            Vector2 position = mTransform.position;
            position.x += 2.1f * (mFacingDirection == DIRECTION.RIGHT ? 1 : -1);
            b_BasicAttackBehavior.Attack(position, 0.1f);
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
            b_BasicAttackBehavior.SetIsFacingRight(true);
        }
        if (b_PhysicsBehavior.GetVelocity().x < 0)
        {
            mFacingDirection = DIRECTION.LEFT;
            mTargetingVector = Vector2.left;
            b_BasicAttackBehavior.SetIsFacingRight(false);
        }
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
}
