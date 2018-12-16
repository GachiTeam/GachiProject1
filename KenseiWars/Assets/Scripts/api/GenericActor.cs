using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActor : MonoBehaviour
{
    protected List<GenericBehavior> mBehaviorsList = new List<GenericBehavior>();
	// Use this for initialization
	void Start ()
    {
        StartActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].StartBehavior();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].UpdateBehavior();
        }
	}

    void FixedUpdate()
    {
        FixedUpdateActor();
        for (int i = 0; i < mBehaviorsList.Count; ++i)
        {
            mBehaviorsList[i].FixedUpdateBehavior();
        }
    }

    protected virtual void StartActor(){}
    protected virtual void UpdateActor(){}
    protected virtual void FixedUpdateActor(){ }

    public virtual Animator GetAnimator()
    {
        Debug.LogWarning("GetAnimator nu este implementat");
        return null;
    }
}
