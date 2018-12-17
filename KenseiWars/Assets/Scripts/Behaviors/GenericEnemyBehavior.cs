using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    private Animator mAnimator;
    private GenericActor mActor;

    public PhysicsBehavior b_PhysicsBehavior;
    private AnimatorBehavior b_AnimatorBehavior;

    private Transform mTargetTransform; //initial player
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
    DIRECTION mFacingDirection;

    //Poate vrei sa restrictionezi constructorul default?
    private GenericEnemyBehavior() { }

    //Constructorul bun

    public GenericEnemyBehavior(GameObject _gameObject)
    {
        mGameObject = _gameObject;
        mTransform = mGameObject.transform;
        mSamuraiTransform = PlayerReference.instance.mSamuraiGameObject.transform;
        mArcherTransform = PlayerReference.instance.mArcherGameObject.transform;
        mTargetTransform = PlayerReference.instance.player.transform;
        mActor = mGameObject.GetComponent<GenericActor>();
        mAnimator = mActor.GetAnimator();

        if (mActor == null)
        {
            Debug.LogWarning("Referinta catre Actor din GenericEnemyBehavior este null!!!");
        }

        if (mAnimator == null)
        {
            Debug.LogWarning("Referinta catre Animator din GenericEnemyBehavior este null!");
        }

        b_PhysicsBehavior = new PhysicsBehavior(mGameObject, mGameObject.GetComponent<Rigidbody2D>());
        b_AnimatorBehavior = new AnimatorBehavior(mGameObject, mActor);

        mBehaviorsList.Add(b_PhysicsBehavior);
        mBehaviorsList.Add(b_AnimatorBehavior);
    }

    // Use this for initialization
    protected override void StartMyBehavior()
    {
    }

    // Update is called once per frame
    protected override void UpdateMyBehavior()
    {
        MoveTowardsPlayer();
        UpdateAnimations(); 
    }

    private void MoveTowardsPlayer()
    {
        float distance = mTargetTransform.position.x - mTransform.position.x;
        DIRECTION direction;

        if (distance == 0)
        {
            return;
        }

        if (distance < 0)
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

        if (direction == DIRECTION.LEFT)
        {
            mFacingDirection = DIRECTION.LEFT;
        }
        else if(direction == DIRECTION.RIGHT)
        {
            mFacingDirection = DIRECTION.RIGHT;
        }
    }

    /*
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
    */

    public void IsHit()
    {
        mActor.StartCoroutine(IsHitCoroutine());//hacks dar are sens
    }

    public IEnumerator IsHitCoroutine()
    {
        b_AnimatorBehavior.PlayAnimation(ANIMATION.HIT);

        mHP--;

        if(mHP == 0)
        {
            Object.Destroy(mGameObject);
        }

        SpriteRenderer spriteRendererActor = mAnimator.GetComponent<SpriteRenderer>();
        Material defaultMaterialActor = spriteRendererActor.material;

        //mAnimator.GetComponent<SpriteRenderer>().material = GlobalSpriteReference.instance.EnemyHitMaterial;
        //materialActor = GlobalSpriteReference.instance.EnemyHitMaterial;
        spriteRendererActor.material = GlobalSpriteReference.instance.EnemyHitMaterial;

        yield return new WaitForSeconds(0.05f);

        //materialActor = GlobalSpriteReference.instance.SpriteDefaultMaterial;
        //mAnimator.GetComponent<SpriteRenderer>().material = GlobalSpriteReference.instance.SpriteDefaultMaterial;
        spriteRendererActor.material = defaultMaterialActor;

        //seteaza boolul de "stun"
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
        
        
        yield return new WaitForSeconds(0.05f);
        
        
        yield return new WaitForSeconds(1.5f);
        

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

    void UpdateAnimations()
    {
        bool isMoving = b_PhysicsBehavior.GetVelocity().x != 0;
        b_AnimatorBehavior.SetIsMoving(isMoving);

        bool isGrounded = b_PhysicsBehavior.GetIsGrounded();
        b_AnimatorBehavior.SetIsGrounded(isGrounded);

        b_AnimatorBehavior.SetFacingDirection(mFacingDirection);
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
}
