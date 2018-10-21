using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyBehavior : GenericBehavior
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

    //Poate vrei sa restrictionezi constructorul default?
    private GenericEnemyBehavior() { }

    //Constructorul bun

    public GenericEnemyBehavior(GameObject gameObject, Sprite isHitSprite, Sprite normalSprite)
    {
        mGameObject = gameObject;
        mTransform = mGameObject.transform;
        mIsHitSprite = isHitSprite;
        mNormalSprite = normalSprite;
    }

    // Use this for initialization
    protected override void StartMyBehavior()
    {
        b_PhysicsBehavior = new PhysicsBehavior(mGameObject, mGameObject.GetComponent<Rigidbody2D>());
        mTargetTrasform = PlayerReference.instance.player.transform;

        mBehaviorsList.Add(b_PhysicsBehavior);

    }

    // Update is called once per frame
    protected override void UpdateMyBehavior()
    {
        Move();
    }

    private void Move()
    {
        float direction = mTargetTrasform.position.x - mTransform.position.x;
        if (direction < mMaxDistance)
        {
            b_PhysicsBehavior.SetMoving(direction, mMaxSpeed);
        }
    }

    public void IsHit()
    {
        mGameObject.GetComponent<EnemyActor>().StartCoroutine(IsHitCoroutine());//hacks dar are sens
    }

    public IEnumerator IsHitCoroutine()
    {
        mHP--;

        if (mHP == 0)
        {
            Object.Destroy(mGameObject);
        }

        mGameObject.GetComponent<SpriteRenderer>().sprite = mIsHitSprite;

        yield return new WaitForSeconds(0.05f);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mNormalSprite;
    }
}
