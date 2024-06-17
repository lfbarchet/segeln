using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelService : MonoBehaviour
{

    public static WheelService Instance { get; private set; }

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

    public void HandleWheelStateChangeFromLocal(
        WheelState wheelState
    )
    {
        // local unity event
        WheelStateChangedEvent.Instance.Invoke(wheelState);
        // publish to MQTT
        SegelnEventDispatcher.Instance.DispatchWheelStateChangedEvent(wheelState);
    }


    public void HandleWheelStateChangeFromServer(
            WheelState wheelState
        )
    {
        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            print("Skip HandleWheelStateChangeFromServer, because this is the wheel cube");
            return;
        }

        // local unity event
        WheelStateChangedEvent.Instance.Invoke(wheelState);
    }
}