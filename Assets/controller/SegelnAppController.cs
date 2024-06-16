using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Controller;
using PuzzleCubes.Models;
using UnityEngine;

public class SegelnAppController : AppController
{

    public FloatEvent orientationEvent;

    public SegelnAppState appState;


    protected override void Initialize()
    {

        this.state = new SegelnAppState(state);      // HACK: override satte in base class
        appState = state as SegelnAppState;
    }

    // CubeControl directly from the Cube via ZeroMQ
    public void HandleCubeControl(CubeControl cubeControl)
    {
        print("HandleCubeControl");

        if (cubeControl == null) return;

        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            float? orientation = cubeControl.Orientation;
            if (orientation == null)
            {
                print("Cannot get orientation from cubeControl (wheel)");
                return;
            }

            WheelState wheelState = new()
            {
                Orientation = orientation.Value,
                Timestamp = cubeControl.Timestamp,
            };
            WheelService.Instance.HandleWheelStateChangeFromLocal(wheelState);
        }
        else
        {
            print("Cannot process cubeControl, because CubeRole is not Wheel");
            return;
        }
    }

}