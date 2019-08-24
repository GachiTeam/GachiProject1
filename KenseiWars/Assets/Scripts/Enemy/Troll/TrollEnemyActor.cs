using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollEnemyActor : GenericActor
                             //, IHitable
{
    private GenericEnemyBehavior b_GenericEnemyBehavior;

    private Transform mTargetTransform;

    protected override void StartActor()
    {
        b_GenericEnemyBehavior = new GenericEnemyBehavior(gameObject);

        mBehaviorsList.Add(b_GenericEnemyBehavior);


        mTargetTransform = PlayerReference.instance.player.transform;
    }

    public override Animator GetAnimator()
    {
        return transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }
}
