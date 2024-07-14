using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    [SerializeField]
    private PerformanceEventType eventType = PerformanceEventType.SEA_MONSTER;


    private void OnTriggerEnter(Collider other)
    {
        EnterSlowDownZone();

    }

    private void OnTriggerExit(Collider other)
    {
        ExitSlowDownZone();
    }

    private void EnterSlowDownZone()
    {
        print("EventZone: Entered slow down zone");
        PerformanceEventState state = new()
        {
            Type = eventType,
            IsStart = true,
            Timestamp = DateTime.UtcNow
        };

        PerformanceEventService.Instance.HandlePerformanceEventStateChangeFromLocal(
            state
        );
    }

    private void ExitSlowDownZone()
    {
        print("EventZone: Exited slow down zone");
        // Do we need this, or is this handled by the web app
    }
}
