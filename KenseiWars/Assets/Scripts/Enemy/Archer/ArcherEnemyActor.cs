using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyActor : GenericActor
                              , IHitable
{
    public Transform mShootingPrefab;
    private GenericEnemyBehavior b_GenericEnemyBehavior;
    private ShootingBehavior b_ShootingBehavior;
    private Transform mTargetTransform;

    private float mRangedDistance = 5;

    //cooldown
    private bool mIsShootingOnCooldown = false;
    private float mShootingCooldownTime = 0.5f;

    protected override void StartActor()
    {
        mTargetTransform = PlayerReference.instance.player.transform;

        b_GenericEnemyBehavior = new GenericEnemyBehavior(gameObject);
        b_ShootingBehavior = new ShootingBehavior(mShootingPrefab);

        b_GenericEnemyBehavior.SetMinDistance(mRangedDistance);
        b_ShootingBehavior.AddHitableTag("player");

        mBehaviorsList.Add(b_GenericEnemyBehavior);
        mBehaviorsList.Add(b_ShootingBehavior);
    }

    protected override void UpdateActor()
    {
        Shoot();
    }

    private void Shoot()
    {

        float distanceToTarget = Mathf.Abs(mTargetTransform.position.x - transform.position.x);
        if (distanceToTarget <= mRangedDistance && mIsShootingOnCooldown == false)
        {
            //Debug.Log()
            b_ShootingBehavior.Shoot(transform.position, (mTargetTransform.position - transform.position).normalized, 25f, 5f);

            StartCoroutine(ShootingOnCooldown());
        }
    }


    public override Animator GetAnimator()
    {
        return transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    private IEnumerator ShootingOnCooldown()
    {
        mIsShootingOnCooldown = true;

        yield return new WaitForSeconds(mShootingCooldownTime);

        mIsShootingOnCooldown = false;
    }

    void IHitable.IsHit()
    {
        b_GenericEnemyBehavior.IsHit();
    }
}
