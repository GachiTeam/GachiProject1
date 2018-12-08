using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ANIMATION : int
{
    ATTACK = 1,
    HIT = 2,
    RUN = 3
};

public class AnimatorBehavior : GenericBehavior
{
    //referinte
    private Animator mAnimator;
    private bool mIsMoving;
    private bool mIsGrounded;
    private DIRECTION mFacingDirection;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private AnimatorBehavior() { }

    //Constructorul bun

    public AnimatorBehavior(GameObject _gameObject, GenericActor _actor)
    {
        mGameObject = _gameObject;
        mTransform = mGameObject.transform;

        mAnimator = _actor.GetAnimator();

        mAnimator.SetBool("isMoving", false);
        mAnimator.SetBool("isGrounded", false);

        mFacingDirection = DIRECTION.RIGHT;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior()
    {
    }

    public void PlayAnimation(ANIMATION _animation)
    {
        switch(_animation)
        {
            case ANIMATION.HIT:
                mAnimator.SetTrigger("isSpecialEvent");
                mAnimator.SetTrigger("isHit");
                break;
            case ANIMATION.ATTACK:
                mAnimator.SetTrigger("isSpecialEvent");
                mAnimator.SetTrigger("isAttacking");
                break;
            default:
                Debug.LogWarning("animatie neimplementata");
                break;
        }
    }

    public void IsMoving(bool _isMoving)
    {
        if (mIsMoving != _isMoving)
        {
            mIsMoving = _isMoving;
            mAnimator.SetBool("isMoving", mIsMoving);
        }
    }

    public void IsGrounded(bool _isGrounded)
    {
        if (mIsGrounded != _isGrounded)
        {
            mIsGrounded = _isGrounded;
            mAnimator.SetBool("isGrounded", mIsGrounded);
        }
    }

    public void SetFacingDirection(DIRECTION _direction)
    {
        if (_direction != mFacingDirection)
        {
            mFacingDirection = _direction;
            FaceDirection(mFacingDirection);
        }
    }

    void FaceDirection(DIRECTION _direction)
    {
        Transform animatorParent = mAnimator.transform.parent;

        animatorParent.Rotate(new Vector3(animatorParent.eulerAngles.x, 180, animatorParent.eulerAngles.z));
    }
}
