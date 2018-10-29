using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : GenericActor
{
    //referinte la componente ale obiectului
    public Sprite mIsHitSprite; //sprite de lovit
    public Sprite mNormalSprite; //sprite normal

    private PhysicsBehavior b_PhysicsBehavior;
    private Transform mTargetTrasform; //initial player


    //proprietati
    public float mHP = 5;
    public float mMaxSpeed = 3;
    public float mJumpTakeOffSpeed = 7;
    public float mMaxDistance = 10;
    private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;

    // Use this for initialization
    protected override void StartActor()
    {
        b_PhysicsBehavior = new PhysicsBehavior(gameObject, GetComponent<Rigidbody2D>());
        mTargetTrasform = PlayerReference.instance.player.transform;

        mBehaviorsList.Add(b_PhysicsBehavior);

    }

    // Update is called once per frame
    protected override void UpdateActor()
    {
        Move();
	}

    private void Move()
    {
        float direction = mTargetTrasform.position.x - transform.position.x;
        if (direction < mMaxDistance)
        {
            b_PhysicsBehavior.SetMoving(direction, mMaxSpeed);
        }
    }

    public void IsHit()
    {
        StartCoroutine(IsHitCoroutine());
    }

    public IEnumerator IsHitCoroutine()
    {
        mHP--;

        if (mHP == 0)
        {
            Destroy(gameObject);
        }

        GetComponent<SpriteRenderer>().sprite = mIsHitSprite;

        yield return new WaitForSeconds(0.05f);

        GetComponent<SpriteRenderer>().sprite = mNormalSprite;
    }
}
