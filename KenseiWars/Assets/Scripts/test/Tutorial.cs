using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject parentGameObject;
    private Transform parentTransform;
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(transform.position.x);
    }

    void FixedUpdate()
    {/*
        bool moveHorizontal = Input.GetKeyDown(KeyCode.UpArrow);
        bool moveVertical = Input.GetButton(Input.GetKeyDown(KeyCode.Space));
        
        */
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            /*
            Vector3 movement = new Vector3(1, 0.0f, 0.0f);

            rb.AddForce(movement);
            */
        }
        else
        {
            /*
            Vector3 movement = new Vector3(-1, 0.0f, 0.0f);

            rb.AddForce(movement);
            */
        }
    }
}
