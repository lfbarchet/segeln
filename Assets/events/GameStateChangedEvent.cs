using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateChangedEvent : UnityEvent<GameState>
{
    private static GameStateChangedEvent instance = new GameStateChangedEvent();
    public static GameStateChangedEvent Instance { get => instance; }
}
