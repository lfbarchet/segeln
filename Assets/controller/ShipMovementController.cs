using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;

class ShipMovementController : MonoBehaviour
{

    public Transform playerCube;
    public Transform wheelCube;

    public Camera sailCamera;

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

    private void FixedUpdate()
    {
        sailCamera.transform.position = new Vector3(playerCube.position.x, sailCamera.transform.position.y, playerCube.position.z);
    }

    private void OnWheelStateChanged(WheelState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Time: {state.Timestamp}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation, 0);

        float angleDiff = state.Orientation - wheelCube.localEulerAngles.z;
        wheelCube.Rotate(Vector3.forward, angleDiff, Space.Self);
    }

    private void OnSailStateChanged(SailState state)
    {
        Debug.Log($"Speed: {state.Speed}, Time: {state.Timestamp}");

        // speed is between 0 and 1
        Vector3 forceDirection = playerCube.forward * state.Speed;
        playerCube.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Impulse);
    }
}