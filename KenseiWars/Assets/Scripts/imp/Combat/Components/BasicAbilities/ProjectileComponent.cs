using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    private float mLifeSpan = 3.0f;
    public float mSpeed = 0.2f;
    private float mTimePassed = 0.0f;
    private Vector2 mDirection = Vector2.zero;

	// Use this for initialization
	void Start ()
    {}
	
	// Update is called once per frame
	void Update ()
    {
        mTimePassed += Time.deltaTime;
        if(mLifeSpan < mTimePassed)
        {
            Destroy(gameObject);
        }

        //misc proiectilul
        transform.Translate(mDirection * mSpeed * Time.deltaTime);
    }

    //setteri

    public void SetDirection(Vector2 _direction)
    {
        mDirection = _direction;
    }

    public void SetProjectileSpeed(float _speed)
    {
        mSpeed = _speed;
    }

    public void SetLifeSpan(float _time)
    {
        mLifeSpan = _time;
    }
}
