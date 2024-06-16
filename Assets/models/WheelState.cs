using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class WheelState
{
    private float orientation;
    private float time;

    public float Orientation { get => orientation; set => orientation = value; }
    public float Time { get => time; set => time = value; }
}
