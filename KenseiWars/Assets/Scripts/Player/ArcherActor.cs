using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherActor: GenericActor
                        , IPlayer
                        , IHitable
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
    private float mRangedAttackCooldownTime = 1f;
    //private float mRangedAttackPassedTime = 0;

    //cooldown
    private bool mIsOilPotOnCooldown = false;
    private float mOilPotCooldownTime = 7f;
    //private float mOilPotCooldownPassedTime = 0;

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
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    protected override void UpdateActor()
    {
        Shoot();
        OilPot();
        //Cooldown();
    }

    void Shoot()
    {
        if (Input.GetAxis(mInput.x) != 0 && b_PlayerBehavior.GetIsTargeting() == true && mIsRangedAttackOnCooldown == false)
        {
            Vector2 direction;
            /*
            if (Input.GetAxis(mInput.joystickHorizontal) == 0 && Input.GetAxis(mInput.joystickVertical) == 0)
            {
                direction = new Vector2(b_PlayerBehavior.IsFacingDirectionRight() == true ? 1 : -1, 0);
            }
            else
            {
                direction = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            }
            */
            b_ShootingBehavior.Shoot(transform.position, b_PlayerBehavior.GetTargetingVector(), 25f, 5f);

            //mIsRangedAttackOnCooldown = true;
            StartCoroutine(RangedAttackOnCooldown());
        }

        if(b_PlayerBehavior.GetIsTargeting() == true)
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
        if (Input.GetAxis(mInput.b) != 0 && mIsOilPotOnCooldown == false  && b_PlayerBehavior.GetIsTargeting() == true)
        {
            Vector2 direction = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));
            //caca
            if (Input.GetAxis(mInput.joystickHorizontal) == 0 && Input.GetAxis(mInput.joystickVertical) == 0)
            {
                direction = new Vector2(b_PlayerBehavior.IsFacingDirectionRight() == true ? 1 : -1, 0);
            }
            b_OilPotBehavior.Shoot(new Vector2(transform.position.x, transform.position.y), direction, 3f);

            //mIsOilPotOnCooldown = true;
            StartCoroutine(OilPotOnCooldown());
        }
    }

    void IHitable.IsHit()
    {
        b_PlayerBehavior.IsHit();
    }

    IEnumerator RangedAttackOnCooldown()
    {
        mIsRangedAttackOnCooldown = true;

        yield return new WaitForSeconds(mRangedAttackCooldownTime);

        mIsRangedAttackOnCooldown = false;
    }

    IEnumerator OilPotOnCooldown()
    {
        mIsOilPotOnCooldown = true;

        yield return new WaitForSeconds(mOilPotCooldownTime);

        mIsOilPotOnCooldown = false;
    }

    /*
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

        if (mIsOilPotOnCooldown == true)
        {
            mOilPotCooldownPassedTime += Time.deltaTime;
            if (mOilPotCooldownPassedTime > mOilPotCooldownTime)
            {
                mOilPotCooldownPassedTime = 0;
                mIsOilPotOnCooldown = false;
            }
        }
    }
    */

    public override Animator GetAnimator()
    {
        return transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }
}
