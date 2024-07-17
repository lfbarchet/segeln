using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateService : MonoBehaviour
{

    public static GameStateService Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private DateTime lastCubeControlTimestamp;
    // every 200ms
    private readonly float cubeControlInterval = 0.2f;

    public void HandleGameStateChangeFromLocal(
        GameState gameState
    )
    {
        // local unity event
        GameStateChangedEvent.Instance.Invoke(gameState);


        var diff = DateTime.UtcNow - lastCubeControlTimestamp;
        if (diff.TotalSeconds < cubeControlInterval)
        {
            return;
        }

        lastCubeControlTimestamp = DateTime.UtcNow;

        // publish to MQTT
        SegelnEventDispatcher.Instance.DispatchGameStateEvent(gameState);
    }

    public void HandleGameStateChangeFromServer(
            GameState gameState
        )
    {
        if (GameManager.Instance.IsMainRole())
        {
            print("Skip HandleGameStateChangeFromServer, because this is the Main cube");
            return;
        }

        // local unity event
        GameStateChangedEvent.Instance.Invoke(gameState);
    }

    public void SendGameOver()
    {
        GameState gameState = new GameState()
        {
            Damage = 1_000_000,
            ShipPositionX = 0,
            ShipPositionY = 0,
            ShipPositionZ = 0,
            ShipRotationX = 0,
            ShipRotationY = 0,
            ShipRotationZ = 0,
            Timestamp = DateTime.UtcNow
        };

        HandleGameStateChangeFromLocal(
            gameState
        );
    }
}