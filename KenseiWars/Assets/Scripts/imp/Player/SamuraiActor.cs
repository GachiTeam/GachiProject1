using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiActor : GenericActor
                          , IPlayer
                          , IHitable
{ 
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;
    public Transform mFireBreathPrefab;
    private InputManagerData mInput;

    private PlayerBehavior b_PlayerBehavior;
    private FireBreathBehavior b_FireBreathBehavior;

    //cooldown
    private bool mIsFireBreathOnCooldown = false;
    private float mFireBreathCooldownPassedTime = 0;
    private float mFireBreathCooldownTime = 7f;

    private bool mIsFireBreathActive = false;
    private float mFireBreathTime = 1.5f;
    private float mFireBreathPassedTime = 0;

    protected override void StartActor()
    {
        b_PlayerBehavior = new PlayerBehavior(gameObject, mBasicAttackPrefab);
        b_FireBreathBehavior = new FireBreathBehavior(mFireBreathPrefab);
        b_PlayerBehavior.SetInput(1);
        mInput = b_PlayerBehavior.mInput;

        mBehaviorsList.Add(b_PlayerBehavior);
        mBehaviorsList.Add(b_FireBreathBehavior);

        b_PlayerBehavior.SetMeleAttackTime(0.3f);
        b_PlayerBehavior.b_BasicAttackBehavior.SetMaxComboNumber(3);
    }

    protected override void UpdateActor()
    {
        FireBreath();
        Cooldown();
    }

    void FireBreath()
    {
        if(Input.GetAxis(mInput.b) != 0 && mIsFireBreathOnCooldown ==false && b_PlayerBehavior.mIsTargeting == true)
        {
            Transform targetingTransform = transform.GetChild(0);

            b_FireBreathBehavior.SetEulerAngle(targetingTransform.eulerAngles);
            b_FireBreathBehavior.UsingFireBreath(transform.position, 1.5f);

            mIsFireBreathOnCooldown = true;
            mIsFireBreathActive = true;

            b_PlayerBehavior.SetCanMove(false);
        }
    }

    void Cooldown()
    {
        //Debug.Log(mRangedAttackPassedTime + " " + mRangedAttackTime);
        if (mIsFireBreathOnCooldown == true)
        {
            mFireBreathCooldownPassedTime += Time.deltaTime;
            if (mFireBreathCooldownPassedTime > mFireBreathCooldownTime)
            {
                mFireBreathCooldownPassedTime = 0;
                mIsFireBreathOnCooldown = false;
            }
        }

        if (mIsFireBreathActive == true)
        {
            mFireBreathPassedTime += Time.deltaTime;
            if (mFireBreathPassedTime > mFireBreathTime)
            {
                mFireBreathPassedTime = 0;
                mIsFireBreathActive = false;

                b_PlayerBehavior.SetCanMove(true);
            }
        }
    }

    void IHitable.IsHit()
    {
        b_PlayerBehavior.IsHit();
    }
}
