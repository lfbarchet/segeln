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
        print("HandleCubeControl");

        if (cubeControl == null) return;

        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            WheelService.Instance.HandleCubeControl(cubeControl);
        }
        else
        {
            print("Cannot process cubeControl, because CubeRole is not Wheel");
            return;
        }
    }

}