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

    public void HandlePerformanceEventStateChangeFromServer(
            PerformanceEventState performanceEventState
        )
    {
        // local unity event
        PerformanceEventStateChangedEvent.Instance.Invoke(performanceEventState);
    }
}