using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBehavior : GenericBehavior
{
    //referinte
    private Transform mBasicAttackPrefab;

    //proprietati
    private float mMeleAttackTime;
    private float mComboTime;
    private float mComboBetweenTime = 2f;
    private int mMaxCombo = 1;

    //stare
    private bool mIsMeleAttackOnCooldown = false;
    private float mMeleAttackPassedTime = 0;
    private bool mCanMeleAttack = true;
    private bool mIsFaceingDirectionRight;

    //combo
    private int mComboID = 1;
    private float mComboPassedTime = 0;
    private float mComboBetweenPassedTime = 0;
    private bool mIsComboOnCooldown = false;

    //animatie


    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private BasicAttackBehavior() { }

    //Constructorul bun

    public BasicAttackBehavior(Transform _transform)
    {
        mBasicAttackPrefab = _transform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior()
    {
        Cooldown();
    }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void Attack(Vector2 _position, float _lifeSpam)
    {
        if (mIsMeleAttackOnCooldown == false && mCanMeleAttack == true && mIsComboOnCooldown == false)
        {
            BasicAttackComponent basicAttackInstance;
            Transform basicAttackTransform;

            basicAttackTransform = Object.Instantiate(mBasicAttackPrefab, _position, Quaternion.identity);
            basicAttackInstance = basicAttackTransform.gameObject.GetComponent<BasicAttackComponent>();

            basicAttackInstance.SetLifeSpan(_lifeSpam);

            AttackSprite(basicAttackInstance.GetComponent<SpriteRenderer>());

            ComboHandle();

            mIsMeleAttackOnCooldown = true;
        }
    }

    private void AttackSprite(SpriteRenderer _spriteRenderer)
    {
        Sprite attackSprite = GlobalSpriteReference.instance.Attack1;
        if (mIsFaceingDirectionRight == true)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }

        switch(mComboID)
        {
            case 1:
                attackSprite = GlobalSpriteReference.instance.Attack1;
                break;
            case 2:
                attackSprite = GlobalSpriteReference.instance.Attack2;
                break;
            case 3:
                attackSprite = GlobalSpriteReference.instance.Attack3;
                break;
            default:
                //Debug.Log("BasicAttackBehavior :: AttackSprite :: EROARE!!! Am primit un combo ID nedefinit");
                break;
        }
        _spriteRenderer.sprite = attackSprite;
    }

    void ComboHandle()
    {
        if(mComboBetweenPassedTime < mComboBetweenTime)
        {
            mComboID++;
        }
        else
        {
            mComboID = 1;
        }

        mComboBetweenPassedTime = 0;

        if (mComboID == mMaxCombo + 1)
        {
            mComboID = 1;
            mIsComboOnCooldown = true;
        }
    }

    void Cooldown()
    {
        if (mIsMeleAttackOnCooldown == true)
        {
            mMeleAttackPassedTime += Time.deltaTime;
            if (mMeleAttackPassedTime > mMeleAttackTime)
            {
                mMeleAttackPassedTime = 0;
                mIsMeleAttackOnCooldown = false;
            }
        }

        if (mIsComboOnCooldown == true)
        {
            mComboPassedTime += Time.deltaTime;
            if (mComboPassedTime > mComboTime)
            {
                mComboPassedTime = 0;
                mIsComboOnCooldown = false;
            }
        }

        mComboBetweenPassedTime += Time.deltaTime;
    }

    public void SetMeleAttackTime(float _meleAttackTime)
    {
        mMeleAttackTime = _meleAttackTime;
    }

    public void SetComboTime(float _comboTime)
    {
        mComboTime = _comboTime;
    }

    public void SetCanMeleAttack(bool _canMeleAttack)
    {
        mCanMeleAttack = _canMeleAttack;
    }

    public void SetIsFacingRight(bool _isFacingRight)
    {
        mIsFaceingDirectionRight = _isFacingRight;
    }

    public void SetMaxComboNumber(int _maxCombo)
    {
        mMaxCombo = _maxCombo;
    }

    public void SetMeleAttackPassedTime(float _meleAttackPassedTime)
    {
        mMeleAttackPassedTime = _meleAttackPassedTime;
    }
    /*
    public void SetComboTime(float _comboTime)
    {
        mComboTime = _comboTime;
    }*/
}
