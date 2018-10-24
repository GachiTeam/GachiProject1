using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiActor : GenericActor
                          , IPlayer
                          , IHitable
{ 
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;

    private PlayerBehavior b_PlayerBehavior;

    protected override void StartActor()
    {
        b_PlayerBehavior = new PlayerBehavior(gameObject, mBasicAttackPrefab);
        b_PlayerBehavior.SetInput(1);

        mBehaviorsList.Add(b_PlayerBehavior);

        b_PlayerBehavior.SetMeleAttackTime(0.4f);
        b_PlayerBehavior.b_BasicAttackBehavior.SetMaxComboNumber(3);
    }

    void IHitable.IsHit()
    {

    }
}
