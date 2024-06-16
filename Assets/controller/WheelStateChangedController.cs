using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;

class WheelStateChangedController : MonoBehaviour
{

    public Transform playerCube;

    private void OnEnable()
    {
        WheelStateChangedEvent.Instance.AddListener(OnWheelStateChanged);
    }

    private void OnDisable()
    {
        WheelStateChangedEvent.Instance.RemoveListener(OnWheelStateChanged);
    }

    private void OnWheelStateChanged(WheelState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Time: {state.Time}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation, 0);
    }
}