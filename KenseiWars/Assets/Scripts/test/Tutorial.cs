using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject parentGameObject;
    private Transform parentTransform;
    public float speed;

    bool jumpImpulse = false;
    bool groundState = false;
    bool jumpState = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
    }

    void Update()
    {
        if(jumpImpulse && groundState)
        {
            //rb.AddForce(new Vector2(0.0f, 5.0f));
            rb.AddForce(Vector2.up * 300);
            groundState = false;
            jumpState = true;
        }

        if(jumpImpulse && jumpState)
        {
            //rb.AddForce(new Vector2(0.0f, 5.0f));
            rb.AddForce(Vector2.up * 300);
            jumpState = false;
        }
    }

    void FixedUpdate()
    {
        jumpImpulse = Input.GetKeyDown("space");
        if(jumpImpulse)
        {
            Debug.Log("space");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground" )
        {
            groundState = true;
        }
    }

        

}
