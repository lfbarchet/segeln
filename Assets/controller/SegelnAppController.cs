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

}