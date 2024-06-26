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
    private readonly float SPEED_MULTIPLIER = 50.0f;



    private CharacterController characterController;
    private float baseRotation = 0.0f;

    private void OnEnable()
    {
        WheelStateChangedEvent.Instance.AddListener(OnWheelStateChanged);
        SailStateChangedEvent.Instance.AddListener(OnSailStateChanged);
        PerformanceEventStateChangedEvent.Instance.AddListener(OnPerformanceEventStateChanged);
    }

    private void OnDisable()
    {
        WheelStateChangedEvent.Instance.RemoveListener(OnWheelStateChanged);
        SailStateChangedEvent.Instance.RemoveListener(OnSailStateChanged);
        PerformanceEventStateChangedEvent.Instance.RemoveListener(OnPerformanceEventStateChanged);
    }


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        baseRotation = playerCube.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        characterController.Move(playerCube.forward * speed * SPEED_MULTIPLIER * Time.deltaTime);
        // sailCamera.transform.position = new Vector3(playerCube.position.x, sailCamera.transform.position.y, playerCube.position.z -8);
    }

    private void OnWheelStateChanged(WheelState state)
    {
        Debug.Log($"Orientation: {state.Orientation}, Time: {state.Timestamp}");

        // orientation is between -180 and 179
        playerCube.rotation = Quaternion.Euler(0, -1 * state.Orientation + baseRotation, 0);

        wheelCube.localEulerAngles = new Vector3(-state.Orientation, 90, -90);
    }

    private void OnSailStateChanged(SailState state)
    {
        Debug.Log($"Speed: {state.Speed}, Time: {state.Timestamp}");

        speed = state.Speed;
    }

    private void OnPerformanceEventStateChanged(PerformanceEventState state)
    {
        Debug.Log($"Performance: {state.Type}, Value: {state.Value}, Time: {state.Timestamp}");

        GameManager.Instance.SetGameSpeed(state.Value);
    }
}