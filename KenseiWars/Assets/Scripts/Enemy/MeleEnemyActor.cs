using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyActor : GenericActor
                            , IHitable
                            , IFireBreathHitable
                            , IOilStainHitable
{
    //referinte la componente ale obiectului
    public Sprite mIsHitSprite; //sprite de lovit
    public Sprite mNormalSprite; //sprite normal
    public Transform mBasicAttackPrefab;

    private GenericEnemyBehavior b_GenericEnemyActor;
    private Transform mTargetTrasform;

    protected override void StartActor()
    {
        b_GenericEnemyActor = new GenericEnemyBehavior(gameObject, mBasicAttackPrefab);
        b_GenericEnemyActor.SetMinDistance(Randomizer(1.7f, 2f));
        b_GenericEnemyActor.SetHP(Randomizer(7f, 10f));
        b_GenericEnemyActor.SetMaxSpeed(Randomizer(0.5f, 1f));

        b_GenericEnemyActor.SetMeleAttackTime(0.7f);
        //b_GenericEnemyActor.b_BasicAttackBehavior.SetMaxComboNumber(3);

        mBehaviorsList.Add(b_GenericEnemyActor);
    }

    float Randomizer(float _rangeBeg, float _rangeEnd)
    {
        float value = _rangeEnd - _rangeBeg + 1;
        value = value * Random.value;
        return _rangeBeg + value;
    }

    void IHitable.IsHit()
    {
        b_GenericEnemyActor.IsHit();
    }

    void IFireBreathHitable.IsHitByFireBreath()
    {
        b_GenericEnemyActor.IsHitByFireBreath();
    }

    void IOilStainHitable.IsInOilStain()
    {
        b_GenericEnemyActor.mMaxSpeed /= 2;
    }

    void IOilStainHitable.IsOutOfoilStain()
    {
        b_GenericEnemyActor.mMaxSpeed *= 2;
    }
}
