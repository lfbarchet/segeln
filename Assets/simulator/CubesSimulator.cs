using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using UnityEngine;
using UnityEngine.Events;



public class CubesSimulator : MonoBehaviour
{
    float wheelCubeOrientation = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            wheelCubeOrientation += UnityEngine.Random.Range(0f, .1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            wheelCubeOrientation -= UnityEngine.Random.Range(0f, .1f);
        }
        else
        {
            return;
        }

        if (wheelCubeOrientation >= 360)
        {
            wheelCubeOrientation = 0;
        }
        WheelService.Instance.HandleWheelRawWheelData(wheelCubeOrientation);
    }
}
