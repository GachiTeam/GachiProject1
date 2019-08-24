using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionNavigator : MonoBehaviour
{
    private InputManagerData mInput;
    public Transform mStartingRegion;
    private GenericRegion mActiveRegion;

    Vector2 mTargetingVector;
    private bool mIsTargeting;

    private bool mIsNavigationThroughMapOnCooldown = false;
    private float mNavigationThroughMapCooldownTime = 0.5f;

    void Start ()
    {
        mInput = new InputManagerData(1);
        mActiveRegion = mStartingRegion.GetComponent<GenericRegion>();
        mActiveRegion.SetInRegion();
    }
	

	void Update ()
    {
        Targeting();
        Navigate();
	}

    void Targeting()
    {
        Vector2 rawDirection = new Vector2(Input.GetAxis(mInput.joystickHorizontal), -Input.GetAxis(mInput.joystickVertical));

        mIsTargeting = rawDirection == Vector2.zero ? false : true;

        mTargetingVector = rawDirection.normalized;
    }

    void Navigate()
    {
        if (mIsNavigationThroughMapOnCooldown == false && mIsTargeting == true)
        {
            GenericRegion nextRegion;
            nextRegion = mActiveRegion.GetNextRegion(mTargetingVector);

            if (nextRegion != null)
            {
                mActiveRegion.SetOutOFRegion();
                nextRegion.SetInRegion();
                mActiveRegion = nextRegion;

                StartCoroutine(NavigationThroughMapOnCooldown());
            }
        }

    }

    IEnumerator NavigationThroughMapOnCooldown()
    {
        mIsNavigationThroughMapOnCooldown = true;

        yield return new WaitForSeconds(mNavigationThroughMapCooldownTime);

        mIsNavigationThroughMapOnCooldown = false;
    }
}
 