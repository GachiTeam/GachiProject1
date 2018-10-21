using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position.y * Vector3.up +transform.position.z * Vector3.forward + Vector3.right * player.transform.position.x;
	}
}
