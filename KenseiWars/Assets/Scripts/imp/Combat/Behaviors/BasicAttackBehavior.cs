using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBehavior : GenericBehavior
{
    //referinte
    private Transform mBasicAttackPrefab;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private BasicAttackBehavior() { }

    //Constructorul bun

    public BasicAttackBehavior(Transform _transform)
    {
        mBasicAttackPrefab = _transform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() { }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void Attack(Vector2 _position, float _lifeSpam)
    {
        BasicAttackComponent basicAttackInstance;
        Transform basicAttackTransform;

        basicAttackTransform = Object.Instantiate(mBasicAttackPrefab, _position, Quaternion.identity);
        basicAttackInstance = basicAttackTransform.gameObject.GetComponent<BasicAttackComponent>();

        basicAttackInstance.SetLifeSpan(_lifeSpam);
    }
}
