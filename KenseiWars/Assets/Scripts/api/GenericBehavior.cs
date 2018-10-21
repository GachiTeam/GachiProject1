using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBehavior
{
    //referinte
    protected GameObject mGameObject;
    protected Transform mTransform;
    protected float maimuta = 0;

    //proprietati

    //stare

    //containere
    protected List<GenericBehavior> mBehaviorsList = new List<GenericBehavior>();

    //Poate vrei sa restrictionezi constructorul default?

    //Constructorul bun
    public GenericBehavior()
    {

    }

    //to be called in Start()
    public void StartBehavior()
    {
        StartMyBehavior();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].StartBehavior();
        }
    }

    //to be called in Update()
    public void UpdateBehavior()
    {
        UpdateMyBehavior();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].UpdateBehavior();
        }
    }

    //to be called in FixedUpdate()
    public void FixedUpdateBehavior()
    {
        FixedUpdateMyBehavior();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].FixedUpdateBehavior();
        }
    }

    //to be called in LateUpdate()
    public void LateUpdateBehavior()
    {
        LateUpdateMyBehavior();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].LateUpdateBehavior();
        }
    }

    protected virtual void StartMyBehavior()
    { }

    protected virtual void UpdateMyBehavior()
    { }

    protected virtual void FixedUpdateMyBehavior()
    { }

    protected virtual void LateUpdateMyBehavior()
    { }
}
