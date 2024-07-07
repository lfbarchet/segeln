using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    [SerializeField]
    private PerformanceEventType eventType = PerformanceEventType.SLOW_DOWN;


    private void OnTriggerEnter(Collider other)
    {

        switch (eventType)
        {
            case PerformanceEventType.SLOW_DOWN:
                EnterSlowDownZone();
                break;

            default:
                Debug.LogWarning("EventZone: Unknown event type: " + eventType);
                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        switch (eventType)
        {
            case PerformanceEventType.SLOW_DOWN:
                ExitSlowDownZone();
                break;

            default:
                Debug.LogWarning("EventZone: Unknown event type: " + eventType);
                break;
        }
    }

    private void EnterSlowDownZone()
    {
        print("EventZone: Entered slow down zone");
        GameManager.Instance.SetGameSpeed(0.5f);
    }

    private void ExitSlowDownZone()
    {
        print("EventZone: Exited slow down zone");
        GameManager.Instance.SetGameSpeed(1.0f);
    }
}
