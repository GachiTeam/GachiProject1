using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActor : MonoBehaviour
{
    Transform mSamuraiTransform;
    Transform mArcherTransform;
    Vector3 mLookAtPosition;
    Rigidbody2D mRigidbody;
    float mPlayerCenter_x;
    float mLeftMostPlayer_x;

    public bool mCanMoveLeft = true;
    public bool mCanMoveRight = true;

    //camera things
    //Vector3 mCameraOffSet;
    float smoothSpeed = 0.125f;
    Vector3 mOffset;
    float mCameraLookAtDistance;

    void Start()
    {
        mSamuraiTransform = PlayerReference.instance.mSamuraiGameObject.transform;
        mArcherTransform = PlayerReference.instance.mArcherGameObject.transform;
        mRigidbody = GetComponent<Rigidbody2D>();
        /*
        CalculatePlayerCenter();
        mCameraOffSet = transform.position - new Vector3(mPlayerCenter_x, transform.position.y, transform.position.z);
        */
        mLookAtPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

	void LateUpdate ()
    {
        mCameraLookAtDistance = Vector2.Distance(transform.position, mLookAtPosition);

        CalculatePlayerCenter();
        SetCameraPosition();
    }

    void CalculatePlayerCenter()
    {
        mPlayerCenter_x = (mSamuraiTransform.position.x + mArcherTransform.position.x) / 2 + 1;
        //mLeftMostPlayer_x = mArcherTransform.position.x < mSamuraiTransform.position.x ? mArcherTransform.position.x : mSamuraiTransform.position.x;
        mLookAtPosition.x = mPlayerCenter_x;
    }

    void SetCameraPosition()
    {
        if (mCanMoveLeft == true && mCanMoveRight == true)
        {
            /*
            Vector3 newPos = new Vector3(mPlayerCenter_x, transform.position.y, transform.position.z) + mCameraOffSet;
            //mRigidbody.position = Vector3.MoveTowards(transform.position, new Vector3(mPlayerCenter_x + 1, transform.position.y, transform.position.z), 10 );//* Time.deltaTime
            transform.position = Vector3.Slerp(transform.position, newPos, 0.5f);
            */
            if (mCameraLookAtDistance == 0)
            {
                Vector3 desiredPosition = mLookAtPosition + mOffset;
                Vector3 smoothedPosition = Vector3.Slerp(mLookAtPosition, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(mPlayerCenter_x, transform.position.y, transform.position.z), 1 * Time.deltaTime);//* Time.deltaTime
            }
        }
    }
}
