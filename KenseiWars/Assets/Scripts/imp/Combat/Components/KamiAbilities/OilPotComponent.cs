using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPotComponent : MonoBehaviour
{
    public float mSpeed = 0.2f;
    public Transform mOilStainPrefab;
    private Vector2 mDirection = Vector2.zero;

    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        //misc proiectilul
        transform.Translate(mDirection * mSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OilStainComponent oilStainInstance;
            Transform oilStainTransform;

            oilStainTransform = Object.Instantiate(mOilStainPrefab, transform.position, Quaternion.identity);
            oilStainInstance = oilStainTransform.gameObject.GetComponent<OilStainComponent>();

            oilStainInstance.SetLifeSpan(30f);
            Destroy(gameObject);
        }
    }

    //setteri

    public void SetDirection(Vector2 _direction)
    {
        mDirection = _direction;
    }

    public void SetProjectileSpeed(float _speed)
    {
        //mSpeed = _speed;
    }
}
