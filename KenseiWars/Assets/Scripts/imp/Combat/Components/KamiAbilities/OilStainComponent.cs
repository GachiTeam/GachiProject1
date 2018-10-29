using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilStainComponent : MonoBehaviour
                               , IFireBreathHitable
{
    private float mSpeed = 5f;
    private float mLifeSpan = 3.0f;
    private float mTimePassed = 0.0f;
    private bool mIsGrounded = false;
    private Transform FireAnimation;
    private bool mIsFireOn = false;

    //cooldown
    private bool mIsAttackedByFireCooldown = false;
    private float mOnFireTimePassed = 0;
    private float mOnFireTime = 0.5f;

    // Use this for initialization
    void Start()
    {
        FireAnimation = transform.GetChild(0);
        FireAnimation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()   
    {

        if (mIsGrounded == false)
        {
            transform.Translate(Vector3.down * mSpeed * Time.deltaTime);
        }
        mTimePassed += Time.deltaTime;
        if (mLifeSpan < mTimePassed)
        {
            Destroy(gameObject);
        }

        Cooldown();
    }

    //setteri

    public void SetLifeSpan(float _time)
    {
        mLifeSpan = _time;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*EnemyActor enemy = collision.gameObject.GetComponent<>();
        if(enemy != null)
        {
            //enemy.IsHitByOil();
        }*/
        if(collision.gameObject.tag == "Ground")
        {
            mIsGrounded = true;
        }

        IOilStainHitable enemy = collision.gameObject.GetComponent<IOilStainHitable>();

        if(collision.tag == "enemy")
        {
            enemy.IsInOilStain();
            if(mIsFireOn == true)
            {

            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        IOilStainHitable enemy = collision.gameObject.GetComponent<IOilStainHitable>();
         
        if (collision.tag == "enemy")
        {
            enemy.IsOutOfoilStain();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        IHitable enemy = collision.gameObject.GetComponent<IHitable>();

        if (collision.tag == "enemy" && mIsAttackedByFireCooldown == false)
        {
            enemy.IsHit();
            mIsAttackedByFireCooldown = true;
        }
    }

    void IFireBreathHitable.IsHitByFireBreath()
    {
        FireAnimation.gameObject.SetActive(true);
        mIsFireOn = true;
    }

    void Cooldown()
    {
        //Debug.Log(mOnFireTimePassed+" "+ mOnFireTime);
        //Debug.Log(mRangedAttackPassedTime + " " + mRangedAttackTime);
        if (mIsAttackedByFireCooldown == true)
        {
            mOnFireTimePassed += Time.deltaTime;
            if (mOnFireTimePassed > mOnFireTime)
            {
                mOnFireTimePassed = 0;
                mIsAttackedByFireCooldown = false;
            }
        }
    }
}
