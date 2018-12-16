using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTest : MonoBehaviour
{
    public float mMultiplier;

    private GameObject mSamuraiGameObject;
    private GameObject mArcherGameObject;
    private float mDistance;

	// Use this for initialization
	void Start ()
    {
        mSamuraiGameObject = PlayerReference.instance.mSamuraiGameObject;
        mArcherGameObject = PlayerReference.instance.mArcherGameObject;

        mDistance = mSamuraiGameObject.transform.position.x + (mArcherGameObject.transform.position.x - mSamuraiGameObject.transform.position.x) / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDistance();
	}

    void UpdateDistance()
    {
        float distance = mSamuraiGameObject.transform.position.x + (mArcherGameObject.transform.position.x - mSamuraiGameObject.transform.position.x) / 2;
        if (distance != mDistance)
        {
            transform.position = new Vector3((distance - mDistance) * mMultiplier + transform.position.x, transform.position.y, transform.position.z);
            mDistance = distance;
        }
    }
}
