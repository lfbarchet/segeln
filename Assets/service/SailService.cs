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
        sailState.Timestamp = System.DateTime.UtcNow;

        SailStateChangedEvent.Instance.Invoke(sailState);
        SegelnEventDispatcher.Instance.DispatchSailStateChangedEvent(sailState);
    }


    public void HandleSailStateChangeFromServer(
            SailState sailState
        )
    {
        if (GameManager.Instance.cubeRole == CubeRole.Sail)
        {
            print("Skip HandleSailStateChangeFromServer, because this is the Sail cube");
            return;
        }

        SailStateChangedEvent.Instance.Invoke(sailState);
    }
}