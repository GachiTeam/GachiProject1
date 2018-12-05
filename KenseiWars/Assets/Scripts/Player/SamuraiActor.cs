using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiActor : GenericActor
                          , IPlayer
                          , IHitable
{ 
    //referinte la componente ale obiectului
    private Transform mBasicAttackPrefab;
    private Transform mFireBreathPrefab;
    private InputManagerData mInput;

    private PlayerBehavior b_PlayerBehavior;
    private FireBreathBehavior b_FireBreathBehavior;

    //cooldown
    private bool mIsFireBreathOnCooldown = false;
    private float mFireBreathCooldownTime = 7f;
    //private float mFireBreathCooldownPassedTime = 0;

    //private bool mIsFireBreathActive = false;
    private float mFireBreathActiveTime = 1.5f;
    //private float mFireBreathPassedTime = 0;

    //abilities
    private float mFireBreathLifeSpam = 1.5f;
    private float mFireBreathPosition_Z = -1.1f;

    protected override void StartActor()
    {
        //Setez referintele catre prefaburi
        mBasicAttackPrefab = GlobalPrefabReference.instance.basicAttackPrefab;
        mFireBreathPrefab = GlobalPrefabReference.instance.fireBreathPrefab;

        //Adaug ce behaviors
        b_PlayerBehavior = new PlayerBehavior(gameObject, mBasicAttackPrefab);
        b_FireBreathBehavior = new FireBreathBehavior(gameObject, mFireBreathPrefab);

        mBehaviorsList.Add(b_PlayerBehavior);
        mBehaviorsList.Add(b_FireBreathBehavior);

        //Setup behaviors
        b_PlayerBehavior.SetInput(1);
        b_PlayerBehavior.SetMeleAttackTime(0.3f);
        b_PlayerBehavior.b_BasicAttackBehavior.SetMaxComboNumber(3);

        //Setez membri
        mInput = b_PlayerBehavior.mInput;
    }

    protected override void UpdateActor()
    {
        FireBreath();
        //Cooldown();
    }

    void FireBreath()
    {
        if(Input.GetAxis(mInput.b) != 0 && mIsFireBreathOnCooldown ==false && b_PlayerBehavior.GetIsTargeting() == true)
        {
            Vector3 firebreathPosition = new Vector3(transform.position.x, transform.position.y, mFireBreathPosition_Z);
            b_FireBreathBehavior.UsingFireBreath(firebreathPosition, mFireBreathLifeSpam);

            //mIsFireBreathOnCooldown = true;
            //mIsFireBreathActive = true;
            StartCoroutine(FireBreathOnCooldown());
            StartCoroutine(FireBreathActive());
        }
    }

    IEnumerator FireBreathOnCooldown()
    {
        mIsFireBreathOnCooldown = true;

        yield return new WaitForSeconds(mFireBreathCooldownTime);

        mIsFireBreathOnCooldown = false;
    }

    IEnumerator FireBreathActive()
    {
        b_PlayerBehavior.SetCanMove(false);

        yield return new WaitForSeconds(mFireBreathActiveTime);

        b_PlayerBehavior.SetCanMove(true);
    }

    /*
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
    */

    void IHitable.IsHit()
    {
        b_PlayerBehavior.IsHit();
    }
}
