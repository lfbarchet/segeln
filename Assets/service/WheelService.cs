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
        WheelStateChangedEvent.Instance.Invoke(wheelState);
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

        WheelStateChangedEvent.Instance.Invoke(wheelState);
    }
}