using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    //private HookInstance hookInstance;
    //public GameObject hookPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grab(PlayerController player, Vector3 mousePosition)
    {
        /*
        hookInstance = Instantiate(hookPrefab, player.transform).GetComponent<HookInstance>();
        hookInstance.SetMousePosition(mousePosition);
        */
    }
}
