using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraPathFollower : MonoBehaviour
{
    public CinemachineDollyCart dollyCart;
    public float speed = 500;

    private bool isEndReached = false;

    private CinemachinePathBase path;
    void Start()
    {
        path = dollyCart.m_Path;

        GetComponentInChildren<Camera>().enabled = true;
    }

    void Update()
    {
        if (isEndReached)
        {
            return;
        }

        dollyCart.m_Position += speed * Time.deltaTime;

        if (dollyCart.m_Position >= path.PathLength)
        {
            OnEndReached();
        }
    }

    void OnEndReached()
    {
        isEndReached = true;
        Debug.Log("End reached");


        GameManager.Instance.IsRunning = true;

        gameObject.SetActive(false);

    }
}
