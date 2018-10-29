using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathBehavior : GenericBehavior
{
    //referinte
    private Transform mFireBreathPrefab;
    private Vector3 mEulerAngle;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private FireBreathBehavior() { }

    //Constructorul bun

    public FireBreathBehavior(Transform _transform)
    {
        mFireBreathPrefab = _transform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() { }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void UsingFireBreath(Vector2 _position,  float _lifespam)
    {
        FireBreathComponent FireBreathInstance;
        Transform FireBreathTransform;

        FireBreathTransform = Object.Instantiate(mFireBreathPrefab, new Vector3(_position.x, _position.y, -1.1f), Quaternion.identity);
        FireBreathInstance = FireBreathTransform.gameObject.GetComponent<FireBreathComponent>();

        FireBreathTransform.eulerAngles = mEulerAngle;

        FireBreathInstance.SetLifeSpan(_lifespam);
    }

    public void SetEulerAngle(Vector3 _eulerAngle)
    {
        mEulerAngle = _eulerAngle;
    }
}
