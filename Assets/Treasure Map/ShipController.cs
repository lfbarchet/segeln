using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 20;
    public float turnSpeed = 20;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        characterController.Move(transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
    }
}
