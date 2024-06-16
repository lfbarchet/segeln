using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SailStateChangedEvent : UnityEvent<SailState>
{
    private static SailStateChangedEvent instance = new SailStateChangedEvent();
    public static SailStateChangedEvent Instance { get => instance; }
}
