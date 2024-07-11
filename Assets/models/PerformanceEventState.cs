using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum PerformanceEventType
{

    SEA_MONSTER,
    SIRENE,
    GOAL
}


public class PerformanceEventState
{
    private PerformanceEventType type;
    private bool isStart;
    private DateTime timestamp;

    public PerformanceEventType Type { get => type; set => type = value; }
    public bool IsStart { get => isStart; set => isStart = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
