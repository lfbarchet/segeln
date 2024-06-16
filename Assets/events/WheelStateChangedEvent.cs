using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelStateChangedEvent : UnityEvent<WheelState>
{
    private static WheelStateChangedEvent instance = new WheelStateChangedEvent();
    public static WheelStateChangedEvent Instance { get => instance; }
}
