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

    public void HandleGameStateChangeFromLocal(
        GameState gameState
    )
    {
        // local unity event
        GameStateChangedEvent.Instance.Invoke(gameState);
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
}