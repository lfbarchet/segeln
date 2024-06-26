using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerformanceEventStateChangedEvent : UnityEvent<PerformanceEventState>
{
    private static PerformanceEventStateChangedEvent instance = new PerformanceEventStateChangedEvent();
    public static PerformanceEventStateChangedEvent Instance { get => instance; }
}
