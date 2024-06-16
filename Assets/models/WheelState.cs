using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class WheelState
{
    private float orientation;
    private DateTime timestamp;

    public float Orientation { get => orientation; set => orientation = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
