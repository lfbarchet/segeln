using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using UnityEngine;
using UnityEngine.Events;



public class CubesSimulator : MonoBehaviour
{
    [UnityEngine.SerializeField]
    private float wheelCubeOrientation = 0;
    [UnityEngine.SerializeField]
    private float sailCubeSpeed = 0;


    float timer = 0;
    readonly float interval = 0.025f; // 25 milliseconds

    readonly float maxWheelSpeed = 1f;
    readonly float maxSailSpeed = .01f;

    void Update()
    {


        timer += Time.deltaTime;

        if (timer >= interval)
        {

            if (Input.GetKey(KeyCode.A))
            {
                SimulateWheelCube(true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SimulateWheelCube(false);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                SimulateSailCube(true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SimulateSailCube(false);
            }

            timer = 0; // Reset the timer
        }

    }

    private void SimulateWheelCube(bool isLeft)
    {

        wheelCubeOrientation += (isLeft ? 1 : -1) * UnityEngine.Random.Range(0f, maxWheelSpeed);

        // orientation is between -180 and 179
        if (wheelCubeOrientation >= 180)
        {
            wheelCubeOrientation = -179;
        }
        else if (wheelCubeOrientation <= -180)
        {
            wheelCubeOrientation = 179;
        }


        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            // Simulate ZeroMQ message (local message)
            (SegelnAppController.Instance as SegelnAppController).HandleCubeControl(new CubeControl
            {
                Orientation = wheelCubeOrientation,
                Timestamp = DateTime.UtcNow
            });
        }
        else
        {
            // Simulate and broadcast MQTT message (server message)
            WheelState state = new WheelState
            {
                Orientation = wheelCubeOrientation,
                Timestamp = DateTime.UtcNow
            };

            SegelnEventDispatcher.Instance.DispatchWheelStateChangedEvent(state);
        }
    }

    private void SimulateSailCube(
        bool faster
    )
    {
        sailCubeSpeed += (faster ? 1 : -1) * UnityEngine.Random.Range(0f, maxSailSpeed);

        SailState state = new SailState
        {
            Speed = sailCubeSpeed,
            Timestamp = DateTime.UtcNow
        };

        if (GameManager.Instance.CubeRole == CubeRole.Sail)
        {
            // Simulate ZeroMQ message (local message)
            SailService.Instance.HandleSailStateChangeFromLocal(state);
        }
        else
        {
            // Simulate and broadcast MQTT message (server message)
            SegelnEventDispatcher.Instance.DispatchSailStateChangedEvent(state);
        }
    }
}
