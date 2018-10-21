using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerData
{
    private int joystickId;
    public string a;
    public string b;
    public string x;
    public string y;

    public string triggers;
    public string joystickHorizontal;
    public string joystickVertical;

    InputManagerData() { }

    public InputManagerData(int _joystickId)
    {
        joystickId = _joystickId;
        a = "Joy " + joystickId + " Button 0";
        b = "Joy " + joystickId + " Button 1";
        x = "Joy " + joystickId + " Button 2";
        y = "Joy " + joystickId + " Button 3";

        joystickHorizontal = "Joy " + joystickId + " Axis 1";
        joystickVertical = "Joy " + joystickId + " Axis 2";
        triggers = "Joy " + joystickId + " Axis 3";
    }
}
