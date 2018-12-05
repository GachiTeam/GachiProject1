using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public static GlobalValues instance = null;

    public float playerNormalSpeed;
    public float playerAirSpeed;
    public float enemySpeed;
    public float gravity = -0.6f;
    public float jumpForce = 14;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
