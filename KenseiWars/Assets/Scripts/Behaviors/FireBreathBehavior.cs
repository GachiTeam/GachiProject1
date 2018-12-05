using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathBehavior : GenericBehavior
{
    //referinte
    private Transform mFireBreathPrefab;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private FireBreathBehavior() { }

    //Constructorul bun

    public FireBreathBehavior(GameObject _gameObject, Transform _fireBreathTransform)
    {
        mGameObject = _gameObject;
        mTransform = mGameObject.transform;

        mFireBreathPrefab = _fireBreathTransform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() { }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void UsingFireBreath(Vector3 _firebreathPosition,  float _lifespam)
    {
        FireBreathComponent fireBreathInstance;
        Transform fireBreathTransform;
        Transform targetingTransform;

        fireBreathTransform = Object.Instantiate(mFireBreathPrefab, _firebreathPosition, Quaternion.identity);
        fireBreathInstance = fireBreathTransform.gameObject.GetComponent<FireBreathComponent>();
        targetingTransform = mTransform.GetChild(1);

        fireBreathTransform.eulerAngles = targetingTransform.eulerAngles;
        fireBreathInstance.SetLifeSpan(_lifespam);
    }
}
