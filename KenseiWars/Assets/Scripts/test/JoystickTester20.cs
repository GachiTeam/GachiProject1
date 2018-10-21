﻿// Arowx.com 2013 - free to use and improve!
using UnityEngine;
using System.Collections;
 
public class JoystickTester20 : MonoBehaviour
{

    public TextMesh joysticks;
    public TextMesh[] inputText;
    public TextMesh[] buttonText;

    public int numSticks;

    void Start()
    {
        int i = 1;

        string sticks = "Joysticks\n";

        foreach (string joyName in Input.GetJoystickNames())
        {
            sticks += i.ToString() + ":" + joyName + "\n";
            i++;
        }

        joysticks.text = sticks;

        numSticks = i;
    }

    /*
     * Read all axis of joystick inputs and display them for configuration purposes
     * Requires the following input managers
     *      Joy[N] Axis 1-9
     *      Joy[N] Button 0-20
     */
    void Update()
    {

        for (int i = 1; i <= numSticks; i++)
        {
            string inputs = "Joystick " + i + "\n";

            string stick = "Joy " + i + " Axis ";

            for (int a = 1; a <= 9; a++)
            {
                inputs += "Axis " + a + ":" + Input.GetAxis(stick + a).ToString("0.00") + "\n";
            }

            inputText[i - 1].text = inputs;
        }

        string buttons = "Buttons 3\n";

        for (int b = 0; b <= 10; b++)
        {
            buttons += "Btn " + b + ":" + Input.GetButton("Joy 3 Button " + b) + "\n";
        }

        buttonText[2].text = buttons;

    }
}