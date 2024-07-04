using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum PerformanceEventType
{

    SLOW_DOWN,
}

public class PerformanceEventState
{
    private PerformanceEventType type;
    private float value;
    private DateTime timestamp;

    public PerformanceEventType Type { get => type; set => type = value; }
    public float Value { get => value; set => this.value = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
