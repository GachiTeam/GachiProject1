using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackComponent : MonoBehaviour
{
    private float mLifeSpan = 3.0f;
    private float mTimePassed = 0.0f;

    // Use this for initialization
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        mTimePassed += Time.deltaTime;
        if (mLifeSpan < mTimePassed)
        {
            Destroy(gameObject);
        }

        //transform
    }

    public void SetLifeSpan(float _time)
    {
        mLifeSpan = _time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IHitable enemy = other.gameObject.GetComponent<IHitable>();
        if (enemy != null)
        {
            enemy.IsHit();
        }
    }
}
