using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyActor : GenericActor
{
    //referinte la componente ale obiectului
    public Sprite mIsHitSprite; //sprite de lovit
    public Sprite mNormalSprite; //sprite normal

    private GenericEnemyBehavior b_GenericEnemyActor;
    private Transform mTargetTrasform;

    protected override void StartActor()
    {
        b_GenericEnemyActor = new GenericEnemyBehavior(gameObject, mIsHitSprite, mNormalSprite);

        mBehaviorsList.Add(b_GenericEnemyActor);
    }
}
