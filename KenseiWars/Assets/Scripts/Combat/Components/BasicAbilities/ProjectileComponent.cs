using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    private float mLifeSpan = 3.0f;
    public float mSpeed = 0.2f;
    private float mTimePassed = 0.0f;
    private Vector2 mDirection = Vector2.zero;
    private List<string> mHitableTagList;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < mHitableTagList.Count; ++i)
        {
            if (mHitableTagList[i] == other.tag)
            {
                IHitable enemy = other.gameObject.GetComponent<IHitable>();
                if (enemy != null)
                {
                    enemy.IsHit();
                    Destroy(gameObject);
                }

                break;
            }
        }
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

    public void SetHitableTagList(List<string> _hitableTagList)
    {
        mHitableTagList = new List<string>();
        for (int i = 0; i < _hitableTagList.Count; ++i)
        {
            mHitableTagList.Add(_hitableTagList[i]);
        }
    }
}
