using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPotBehavior : GenericBehavior
{
    //referinte
    private Transform mOilPotPrefab;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private OilPotBehavior() { }

    //Constructorul bun

    public OilPotBehavior(Transform _transform)
    {
        mOilPotPrefab = _transform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() { }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void Shoot(Vector2 _position, Vector2 _direction, float _speed)
    {
        _direction = _direction.normalized;
        OilPotComponent OilPotInstance;
        Transform OilPotTransform;

        OilPotTransform = Object.Instantiate(mOilPotPrefab, new Vector3(_position.x, _position.y, -6), Quaternion.identity);
        OilPotInstance = OilPotTransform.gameObject.GetComponent<OilPotComponent>();

        OilPotInstance.SetProjectileSpeed(_speed);
        OilPotInstance.SetDirection(_direction);
    }
}
