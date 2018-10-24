using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherActor: GenericActor
                        , IPlayer
{
    //referinte la componente ale obiectului
    public Transform mShootingPrefab;
    public Transform mOilPotPrefab;
    public Transform mBasicAttackPrefab;
    private InputManagerData mInput;

    private PlayerBehavior b_PlayerBehavior;
    private ShootingBehavior b_ShootingBehavior;
    private OilPotBehavior b_OilPotBehavior;

    //ranged attack?
    private bool mIsRangedAttackOnCooldown = false;
    private float mRangedAttackPassedTime = 0;
    private float mRangedAttackTime = 1f;

    protected override void StartActor()
    {
        b_PlayerBehavior = new PlayerBehavior(gameObject, mBasicAttackPrefab);
        b_ShootingBehavior = new ShootingBehavior(mShootingPrefab);
        b_OilPotBehavior = new OilPotBehavior(mOilPotPrefab);

        b_PlayerBehavior.SetInput(2);
        mInput = b_PlayerBehavior.mInput;

        mBehaviorsList.Add(b_PlayerBehavior);
        mBehaviorsList.Add(b_ShootingBehavior);
        mBehaviorsList.Add(b_OilPotBehavior);

        b_PlayerBehavior.SetMeleAttackTime(0.6f);
    }

    protected override void UpdateActor()
    {
        Shoot();
        OilPot();
        Cooldown();
    }

    void Shoot()
    {
        if (Input.GetAxis(mInput.x) != 0 && b_PlayerBehavior.mIsTargeting == true && mIsRangedAttackOnCooldown == false)
        {
            Vector2 direction;
            if (Input.GetAxis(mInput.joystickHorizontal) == 0 && Input.GetAxis(mInput.joystickVertical) == 0)
            {
                direction = new Vector2(b_PlayerBehavior.IsFacingDirectionRight() == true ? 1 : -1, 0);
            }
            else
            {
                direction = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            }
            b_ShootingBehavior.Shoot(transform.position, direction, 10f, 5f);

            mIsRangedAttackOnCooldown = true;
        }

        if(b_PlayerBehavior.mIsTargeting == true)
        {
            b_PlayerBehavior.SetCanMeleAttack(false);
        }
        else
        {
            b_PlayerBehavior.SetCanMeleAttack(true);
        }
    }

    void OilPot()
    {
        if (Input.GetAxis(mInput.b) != 0)
        {
            Vector2 direction = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            b_OilPotBehavior.Shoot(new Vector2(transform.position.x, transform.position.y), direction, 1f);
        }
    }

    void Cooldown()
    {
        //Debug.Log(mRangedAttackPassedTime + " " + mRangedAttackTime);
        if (mIsRangedAttackOnCooldown == true)
        {
            mRangedAttackPassedTime += Time.deltaTime;
            if (mRangedAttackPassedTime > mRangedAttackTime)
            {
                mRangedAttackPassedTime = 0;
                mIsRangedAttackOnCooldown = false;
            }
        }
    }
}
