using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavigationStateChangedEvent : UnityEvent<NavigationState>
{
    private static NavigationStateChangedEvent instance = new NavigationStateChangedEvent();
    public static NavigationStateChangedEvent Instance { get => instance; }
}
