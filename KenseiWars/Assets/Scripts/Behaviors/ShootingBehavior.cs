using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : GenericBehavior
{
    //referinte
    private Transform mProjectilePrefab;

    //proprietati

    //stare

    //containere

    //Poate vrei sa restrictionezi constructorul default?
    private ShootingBehavior() {}

    //Constructorul bun

    public ShootingBehavior(Transform _transform)
    {
        mProjectilePrefab = _transform;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() { }

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior() { }

    //to be called in LateUpdate()
    protected override void LateUpdateMyBehavior() { }

    public void Shoot(Vector2 _position, Vector2 _direction, float _speed, float _lifeSpam)
    {
        _direction = _direction.normalized;
        ProjectileComponent projectileInstance;
        Transform projectileTransform;

        projectileTransform = Object.Instantiate(mProjectilePrefab, new Vector3(_position.x, _position.y, -2), Quaternion.identity);
        projectileInstance = projectileTransform.gameObject.GetComponent<ProjectileComponent>();

        projectileInstance.SetLifeSpan(_lifeSpam);
        projectileInstance.SetProjectileSpeed(_speed);
        projectileInstance.SetDirection(_direction);
    }
}
