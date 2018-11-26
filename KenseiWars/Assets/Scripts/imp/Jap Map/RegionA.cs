using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionA : GenericRegion
{
    public Transform mRegionBTransform;
    public Transform mRegionDTransform;

    private GenericRegion mRegionB;
    private GenericRegion mRegionD;

    void Awake()
    {
        mRegionB = mRegionBTransform.GetComponent<GenericRegion>();
        mRegionD = mRegionDTransform.GetComponent<GenericRegion>();

    }


    public override GenericRegion GetNextRegion(Vector2 _targetDirection)
    {
        float angle = 0;
        if (angle >= 0 && angle <= 36)
        {
            return mRegionB;
        }
        else if (angle > 36 && angle <= 113)
        {
            return mRegionD;
        }
        else
        {
            return null;
        }
    }
}
