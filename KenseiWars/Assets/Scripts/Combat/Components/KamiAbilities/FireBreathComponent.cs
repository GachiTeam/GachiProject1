using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathComponent : MonoBehaviour
{
    private float mTimePassed;
    private float mLifeSpan;
    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        mTimePassed += Time.deltaTime;
        if (mLifeSpan < mTimePassed)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy" || other.tag == "oil")
        {
            IFireBreathHitable fireBreathHitable = other.gameObject.GetComponent<IFireBreathHitable>();
            if (fireBreathHitable != null)
            {
                fireBreathHitable.IsHitByFireBreath();
            }
        }
    }

    //setteri

    public void SetLifeSpan(float _time)
    {
        mLifeSpan = _time;
    }
}
