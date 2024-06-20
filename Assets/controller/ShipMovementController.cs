using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;

class ShipMovementController : MonoBehaviour
{

    public Transform playerCube;
    public Transform wheelCube;

    public Camera sailCamera;

    [Header("Speed")]
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private readonly float SPEED_MULTIPLIER = 50.0f;



    private CharacterController characterController;

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


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        characterController.Move(playerCube.forward * speed * SPEED_MULTIPLIER * Time.deltaTime);
        sailCamera.transform.position = new Vector3(playerCube.position.x, sailCamera.transform.position.y, playerCube.position.z);
    }

    private void OnWheelStateChanged(WheelState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Time: {state.Timestamp}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation, 0);

        wheelCube.localEulerAngles = new Vector3(-state.Orientation, 90, -90);

        // float angleDiff = state.Orientation - wheelCube.localEulerAngles.z;
        // wheelCube.Rotate(Vector3.forward, angleDiff, Space.Self);
    }

    private void OnSailStateChanged(SailState state)
    {
        Debug.Log($"Speed: {state.Speed}, Time: {state.Timestamp}");

        // speed is between 0 and 1
        // Vector3 forceDirection = playerCube.forward * state.Speed;
        // playerCube.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Impulse);
        speed = state.Speed;
    }
}