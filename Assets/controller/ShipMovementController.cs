using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;

class ShipMovementController : MonoBehaviour
{

    public Transform playerCube;

    private void OnEnable()
    {
        WheelStateChangedEvent.Instance.AddListener(OnWheelStateChanged);
        SailStateChangedEvent.Instance.AddListener(OnSailStateChanged);
    }

    private void OnDisable()
    {
        WheelStateChangedEvent.Instance.RemoveListener(OnWheelStateChanged);
        SailStateChangedEvent.Instance.RemoveListener(OnSailStateChanged);
    }

    private void OnWheelStateChanged(WheelState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Time: {state.Time}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation, 0);
    }

    private void OnSailStateChanged(SailState state)
    {
        Debug.Log($"Speed: {state.Speed}, Time: {state.Time}");

        // speed is between 0 and 1
        playerCube.position += playerCube.forward * state.Speed;
    }
}