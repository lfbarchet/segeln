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
    float interval = 0.025f; // 25 milliseconds

    float maxWheelSpeed = 1f;
    float maxSailSpeed = .01f;

    void Update()
    {


        timer += Time.deltaTime;

        if (timer >= interval)
        {

            if (Input.GetKey(KeyCode.A))
            {
                simulateWheelCube(true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                simulateWheelCube(false);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                simulateSailCube(true);
            }
            else if (Input.GetKey(KeyCode.H))
            {
                simulateSailCube(false);
            }

            timer = 0; // Reset the timer
        }

    }

    private void simulateWheelCube(bool isLeft)
    {

        wheelCubeOrientation += (isLeft ? 1 : -1) * UnityEngine.Random.Range(0f, maxWheelSpeed);

        if (wheelCubeOrientation >= 360)
        {
            wheelCubeOrientation = 0;
        }

        WheelState state = new WheelState
        {
            Orientation = wheelCubeOrientation,
            Timestamp = DateTime.UtcNow
        };

        if (GameManager.Instance.CubeRole == CubeRole.Wheel)
        {
            // Simulate ZeroMQ message (local message)
            WheelService.Instance.HandleWheelStateChangeFromLocal(state);
        }
        else
        {
            // Simulate and broadcast MQTT message (server message)
            SegelnEventDispatcher.Instance.DispatchWheelStateChangedEvent(state);
        }
    }

    private void simulateSailCube(
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
