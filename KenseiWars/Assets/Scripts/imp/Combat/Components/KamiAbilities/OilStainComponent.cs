using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilStainComponent : MonoBehaviour
{
    private float mSpeed = 5f;
    private float mLifeSpan = 3.0f;
    private float mTimePassed = 0.0f;
    private bool mIsGrounded = false;

    // Use this for initialization
    void Start()
    { }

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
    }

    //setteri

    public void SetLifeSpan(float _time)
    {
        mLifeSpan = _time;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyActor enemy = collision.gameObject.GetComponent<EnemyActor>();
        if(enemy != null)
        {
            //enemy.IsHitByOil();
        }
        if(collision.gameObject.tag == "Ground")
        {
            mIsGrounded = true;
        }
    }
}
