using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickTester : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i <= 19; ++i)
        {
            string myString = "Joy 1 Button " + i;
            if (Input.GetAxis(myString) != 0)
                Debug.Log(myString);
        }
    }
    void Update()
    {
        for (int i = 0; i <= 19; ++i)
        {
            string myString = "Joy 1 Button " + i;
            if (Input.GetAxis(myString) != 0)
                Debug.Log(myString + " " + Input.GetAxis(myString));
        }
        
        for (int i = 1; i <= 9; ++i)
        {
            string myString = "Joy 1 Axis " + i;
            if (Input.GetAxis(myString) != 0)
                Debug.Log(myString + " " + Input.GetAxis(myString));
        }

        for (int i = 0; i <= 19; ++i)
        {
            string myString = "Joy 2 Button " + i;
            if (Input.GetAxis(myString) != 0)
                Debug.Log(myString + " " + Input.GetAxis(myString));
        }

        for (int i = 1; i <= 9; ++i)
        {
            string myString = "Joy 2 Axis " + i;
            if (Input.GetAxis(myString) != 0)
                Debug.Log(myString + " " + Input.GetAxis(myString));
        }
    }

}
