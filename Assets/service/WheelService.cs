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

    public void HandleWheelRawWheelData(
        float orientation
    )
    {

        var newState = new WheelState
        {
            Orientation = orientation

        };

        WheelStateChangedEvent.Instance.Invoke(newState);
    }
}
