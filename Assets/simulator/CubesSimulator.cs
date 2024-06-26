using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;



public class CubesSimulator : MonoBehaviour
{
    public SailController sailController;
    [UnityEngine.SerializeField]
    private float wheelCubeOrientation = 0;
    [UnityEngine.SerializeField]
    private float sailCubeOrientation = 0;
    [UnityEngine.SerializeField]
    private float sailCubeSpeed = 0;


    float timer = 0;
    readonly float interval = 0.025f; // 25 milliseconds

    readonly float maxWheelSpeed = 1f;
    readonly float maxSailSpeed = 1f;
    private Coroutine coroutine;

    private float gameSpeed = 1;

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

        DetectCubeRoleChange();
        DetectEventTrigger();
    }

    private void SimulateWheelCube(bool isLeft)
    {

        wheelCubeOrientation += (isLeft ? 1 : -1) * UnityEngine.Random.Range(0f, maxWheelSpeed);



        WheelService.Instance.HandleCubeControl(new CubeControl
        {
            Orientation = wheelCubeOrientation,
            Timestamp = DateTime.UtcNow
        });
    }

    private void SimulateSailCube(bool isUp)
    {
        sailCubeOrientation += (isUp ? 1 : -1) * maxSailSpeed;

        // orientation is between -180 and 179
        if (sailCubeOrientation >= 180)
        {
            sailCubeOrientation = -179;
        }
        else if (sailCubeOrientation <= -180)
        {
            sailCubeOrientation = 179;
        }
        sailController.HandleOrientation(sailCubeOrientation);
        // sailCubeSpeed += (faster ? 1 : -1) * UnityEngine.Random.Range(0f, maxSailSpeed);
    }

    private IEnumerator SendShipSpeed(float interval)
    {
        while (true)
        {
            SailState state = new()
            {
                Speed = sailController.currentShipSpeed,
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


            yield return new WaitForSeconds(interval);
        }
    }

    private void DetectCubeRoleChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.SetCubeRole(CubeRole.Wheel);
            StopCoroutine(coroutine);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.SetCubeRole(CubeRole.Sail);
            coroutine = StartCoroutine(SendShipSpeed(0.2f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.SetCubeRole(CubeRole.Map);
            StopCoroutine(coroutine);
        }
    }

    private void DetectEventTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameSpeed = gameSpeed == 0.5f ? 0.25f : gameSpeed == 0.25f ? 1f : 0.5f;

            PerformanceEventState state = new()
            {
                Type = PerformanceEventType.SLOW_DOWN,
                Value = gameSpeed,
                Timestamp = DateTime.UtcNow
            };
            SegelnEventDispatcher.Instance.DispatchPerformanceEventStateChangedEvent(state);

        }
    }
}
