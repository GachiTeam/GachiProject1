using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    public Sprite mIsHitSprite; //sprite de lovit
    public Sprite mNormalSprite; //sprite normal

    public PhysicsBehavior b_PhysicsBehavior;
    public BasicAttackBehavior b_BasicAttackBehavior;
    private Transform mTargetTrasform; //initial player
    private bool mIsInMeleRange = false;

    //referinte externe
    private Transform mSamuraiTransform;
    private Transform mArcherTransform;


    //proprietati
    public float mHP = 5;
    public float mMaxSpeed = 3;
    public float mJumpTakeOffSpeed = 7;
    private float mMaxDistance = 10;
    private float mMinDistance;
    //private enum DIRECTION : int { LEFT = 1, RIGHT = 2 };
    DIRECTION mFacingDirection;

    //Poate vrei sa restrictionezi constructorul default?
    private GenericEnemyBehavior() { }

    //Constructorul bun

    public GenericEnemyBehavior(GameObject gameObject, Transform _basicAttackPrefab)
    {
        mGameObject = gameObject;
        mTransform = mGameObject.transform;

        mSamuraiTransform = PlayerReference.instance.mSamuraiGameObject.transform;
        mArcherTransform = PlayerReference.instance.mArcherGameObject.transform;
        
        b_PhysicsBehavior = new PhysicsBehavior(mGameObject, mGameObject.GetComponent<Rigidbody2D>());
        b_BasicAttackBehavior = new BasicAttackBehavior(_basicAttackPrefab);
        mTargetTrasform = PlayerReference.instance.player.transform;
        b_BasicAttackBehavior.AddHitableTag("player");

        mBehaviorsList.Add(b_PhysicsBehavior);
        mBehaviorsList.Add(b_BasicAttackBehavior);

        mIsHitSprite = GlobalSpriteReference.instance.EnemyHit;
        mNormalSprite = GlobalSpriteReference.instance.EnemyNormal;

        //manarie urata
        b_BasicAttackBehavior.mIsEnemy = true;
    }

    // Use this for initialization
    protected override void StartMyBehavior()
    {
    }

    // Update is called once per frame
    protected override void UpdateMyBehavior()
    {
        MoveTowardsPlayer();
        MeleAttack();
    }

    private void MoveTowardsPlayer()
    {
        float distance = mTargetTrasform.position.x - mTransform.position.x;
        DIRECTION direction;
        
        if (distance == 0)
        {
            direction = DIRECTION.DEFAULT;
        }
        else if (distance < 0)
        {
            distance *= -1;
            direction = DIRECTION.LEFT;
        }
        else
        {
            direction = DIRECTION.RIGHT;
        }

        if (distance < mMaxDistance && distance > mMinDistance)
        {
            mIsInMeleRange = false;
            b_PhysicsBehavior.SetMoving(direction, mMaxSpeed);
        }
        else if(distance > mMaxDistance)
        {
            mIsInMeleRange = false;
            //idle movement
        }
        else
        {
            mIsInMeleRange = true;
            b_PhysicsBehavior.SetMoving(0, 0);
        }

        if (direction < 0)
        {
            mFacingDirection = DIRECTION.LEFT;
        }
        else
        {
            mFacingDirection = DIRECTION.RIGHT;
        }
    }

    public void MeleAttack()
    {
        if (mIsInMeleRange == true)
        {
            if(mFacingDirection == DIRECTION.RIGHT)
            {
                b_BasicAttackBehavior.SetIsFacingRight(true);
            }
            else
            {
                b_BasicAttackBehavior.SetIsFacingRight(false);
            }
            Vector2 position = mTransform.position;
            position.x += 2.1f * (mFacingDirection == DIRECTION.RIGHT ? 1 : -1);
            b_BasicAttackBehavior.Attack(position, 0.1f);
        }
    }

    public void IsHit()
    {
        mGameObject.GetComponent<MeleEnemyActor>().StartCoroutine(IsHitCoroutine());//hacks dar are sens
    }

    public IEnumerator IsHitCoroutine()
    {
        mHP--;
        if (mHP < 0)
        {
            Object.Destroy(mGameObject);
        }

        b_BasicAttackBehavior.SetMeleAttackPassedTime(0);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mIsHitSprite;

        yield return new WaitForSeconds(0.05f);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mNormalSprite;
    }

    public void IsHitByFireBreath()
    {
        mGameObject.GetComponent<MeleEnemyActor>().StartCoroutine(IsHitByFireBreathCoroutine());//hacks dar are sens
    }

    public IEnumerator IsHitByFireBreathCoroutine()
    {
        b_PhysicsBehavior.SetCanMove(false);

        mHP -= 3;
        if (mHP < 0)
        {
            Object.Destroy(mGameObject);
        }

        mGameObject.GetComponent<SpriteRenderer>().sprite = mIsHitSprite;
        
        b_BasicAttackBehavior.SetMeleAttackPassedTime(0);
        yield return new WaitForSeconds(0.05f);

        mGameObject.GetComponent<SpriteRenderer>().sprite = mNormalSprite;

        b_BasicAttackBehavior.SetMeleAttackPassedTime(0);
        yield return new WaitForSeconds(1.5f);

        b_BasicAttackBehavior.SetMeleAttackPassedTime(0);

        b_PhysicsBehavior.SetCanMove(true);
    }

    private void UpdateTarget()
    {
        float samuraiDistance = Vector2.Distance(mTransform.position, mSamuraiTransform.position);
        float archerDistance = Vector2.Distance(mTransform.position, mArcherTransform.position);

        if(samuraiDistance < archerDistance)
        {
            mTransform = mSamuraiTransform;
        }
        else
        {
            mTransform = mArcherTransform;
        }
    }

    public void SetMinDistance(float _minDistance)
    {
        mMinDistance = _minDistance;
    }

    public void SetHP(float _hp)
    {
        mHP = _hp;
    }

    public void SetMaxSpeed(float _maxSpeed)
    {
        mMaxSpeed = _maxSpeed;
    }

    public bool GetIsInMeleRange()
    {
        return mIsInMeleRange;
    }

    public void SetMeleAttackTime(float _meleAttackTime)
    {
        b_BasicAttackBehavior.SetMeleAttackTime(_meleAttackTime);
    }
}
