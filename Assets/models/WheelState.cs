using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class WheelState
{
    private float wheelOrientation;
    private float shipOrientation;
    private DateTime timestamp;

    public float WheelOrientation { get => wheelOrientation; set => wheelOrientation = value; }
    public float ShipOrientation { get => shipOrientation; set => shipOrientation = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
