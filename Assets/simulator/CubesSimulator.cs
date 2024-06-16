using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using UnityEngine;
using UnityEngine.Events;



public class CubesSimulator : MonoBehaviour
{
    float wheelCubeOrientation = 0;


    float timer = 0;
    float interval = 0.025f; // 25 milliseconds

    float speed = 1f;

    void Update()
    {


        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0; // Reset the timer

            if (Input.GetKey(KeyCode.A))
            {
                wheelCubeOrientation += UnityEngine.Random.Range(0f, speed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                wheelCubeOrientation -= UnityEngine.Random.Range(0f, speed);
            }
            else
            {
                return;
            }

            if (wheelCubeOrientation >= 360)
            {
                wheelCubeOrientation = 0;
            }


            WheelState state = new WheelState
            {
                Orientation = wheelCubeOrientation
            };

            if (GameManager.Instance.cubeRole == CubeRole.Wheel)
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

    }
}
