

using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using UnityEngine;

public class WheelService : MonoBehaviour
{

    public static WheelService Instance { get; private set; }

    private CubeControl lastCubeControl;

    [SerializeField]
    [Tooltip("The orientation of the wheel in degrees. The orientation is between -inf and +inf.")]
    private float wheelOrientation = 0.0f;

    [SerializeField]
    [Tooltip("The factor to apply to the difference in cube control orientation to calculate the wheel orientation.")]
    [Range(0.01f, 2f)]
    private float wheelFactor = 0.25f;

    [SerializeField]
    private float shipOrientation = 0.0f;


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



    private DateTime lastCubeControlTimestamp;
    // every 200ms
    private readonly float cubeControlInterval = 0.2f;

    public void HandleWheelStateChangeFromLocal(
        WheelState wheelState
    )
    {
        // local unity event
        WheelStateChangedEvent.Instance.Invoke(wheelState);

        var diff = DateTime.UtcNow - lastCubeControlTimestamp;
        if (diff.TotalSeconds < cubeControlInterval)
        {
            return;
        }

        lastCubeControlTimestamp = DateTime.UtcNow;

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

    public void HandleCubeControl(
        CubeControl cubeControl
    )
    {
        if (!cubeControl.Orientation.HasValue)
        {
            Debug.Log("Cannot get orientation from cubeControl (wheel)");
            return;
        }

        float currentOrientation = cubeControl.Orientation.Value;
        float lastOrientation = lastCubeControl != null ? lastCubeControl.Orientation.Value : currentOrientation; // Default to current if last is null
        float rawDiff = currentOrientation - lastOrientation;

        // Adjust for wrap-around
        float diff;
        if (rawDiff > 180)
        {
            diff = rawDiff - 360;
        }
        else if (rawDiff < -180)
        {
            diff = rawDiff + 360;
        }
        else
        {
            diff = rawDiff;
        }


        wheelOrientation += diff;
        shipOrientation += diff * wheelFactor;

        WheelState wheelState = new()
        {
            WheelOrientation = wheelOrientation,
            ShipOrientation = shipOrientation,

            Timestamp = DateTime.Now
        };
        HandleWheelStateChangeFromLocal(wheelState);

        lastCubeControl = cubeControl;
    }
}
