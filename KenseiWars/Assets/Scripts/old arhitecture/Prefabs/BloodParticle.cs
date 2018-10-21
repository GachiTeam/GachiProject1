using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour {

    public float lifeSpan = 0.05f;

    private float timeLived = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLived += Time.deltaTime;

        if (timeLived >= lifeSpan)
            Destroy(this.gameObject);
	}
}
