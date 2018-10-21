using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReference : MonoBehaviour {

    public static PlayerReference instance = null;
    public GameObject player;

    public float playerMaxSpeed;
    public float playerMaxJump;
    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
	
}
