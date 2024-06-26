using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailService : MonoBehaviour
{

    public static SailService Instance { get; private set; }

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

    public void HandleSailStateChangeFromLocal(
        SailState sailState
    )
    {
        // local unity event
        SailStateChangedEvent.Instance.Invoke(sailState);
        // publish to MQTT
        SegelnEventDispatcher.Instance.DispatchSailStateChangedEvent(sailState);
    }


    public void HandleSailStateChangeFromServer(
            SailState sailState
        )
    {
        if (GameManager.Instance.CubeRole == CubeRole.Sail)
        {
            print("Skip HandleSailStateChangeFromServer, because this is the Sail cube");
            return;
        }

        // local unity event
        SailStateChangedEvent.Instance.Invoke(sailState);
    }
}