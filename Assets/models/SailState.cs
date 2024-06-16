using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class SailState
{
    private float speed;
    private DateTime timestamp;

    public float Speed { get => speed; set => speed = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
