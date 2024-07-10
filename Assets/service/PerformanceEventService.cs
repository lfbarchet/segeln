using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceEventService : MonoBehaviour
{

    public static PerformanceEventService Instance { get; private set; }

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

    public void HandlePerformanceEventStateChangeFromLocal(
        PerformanceEventState performanceEventState
    )
    {
        // local unity event
        PerformanceEventStateChangedEvent.Instance.Invoke(performanceEventState);
        // publish to MQTT
        SegelnEventDispatcher.Instance.DispatchPerformanceEventStateChangedEvent(performanceEventState);
    }

    public void HandlePerformanceEventStateChangeFromServer(
            PerformanceEventState performanceEventState
        )
    {
        if (GameManager.Instance.IsMainRole())
        {
            print("Skip HandlePerformanceEventStateChangeFromServer, because this is the Main cube");
            return;
        }

        // local unity event
        PerformanceEventStateChangedEvent.Instance.Invoke(performanceEventState);
    }
}