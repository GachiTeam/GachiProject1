using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiActor : GenericActor
{
    //referinte la componente ale obiectului
    public Transform mBasicAttackPrefab;

    private PlayerBehavior b_PlayerBehavior;

    protected override void StartActor()
    {
        b_PlayerBehavior = new PlayerBehavior(gameObject, mBasicAttackPrefab);
        b_PlayerBehavior.SetInput(1);

        mBehaviorsList.Add(b_PlayerBehavior);
    }
}
