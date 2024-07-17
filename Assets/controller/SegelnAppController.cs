using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Controller;
using PuzzleCubes.Models;
using UnityEngine;

public class SegelnAppController : AppController
{

    public SegelnAppState appState;


    protected override void Initialize()
    {

        this.state = new SegelnAppState(state);      // HACK: override satte in base class
        appState = state as SegelnAppState;
    }

    // CubeControl directly from the Cube via ZeroMQ
    public void HandleCubeControl(CubeControl cubeControl)
    {

        if (cubeControl == null) return;

        if (!GameManager.Instance.IsRunning)
        {
            return;
        }


        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            WheelService.Instance.HandleCubeControl(cubeControl);
            return;
        }

        if (GameManager.Instance.CubeRole == CubeRole.Sail)
        {
            SailService.Instance.HandleCubeControl(cubeControl);
            return;
        }

        print("Cannot process cubeControl, because CubeRole is not implemented: " + GameManager.Instance.CubeRole);
    }

}