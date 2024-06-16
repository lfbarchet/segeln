using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeRole
{
    Wheel,
    Sail,
    Map
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [Header("Cube Role")]
    [SerializeField]
    private CubeRole cubeRole = CubeRole.Map;
    public CubeRole CubeRole { get => cubeRole; set => SetCubeRole(value); }

    [Header("Camera")]
    [SerializeField]
    private Camera sailCamera;
    [SerializeField]
    private Camera wheelCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetCubeRole(cubeRole);
    }

    void OnValidate()
    {
        print("OnValidate: " + cubeRole);
        SetCubeRole(cubeRole);
    }

    void DeactivateAllCameras()
    {
        // Get all cameras in the scene
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
    }

    public void SetCubeRole(CubeRole cubeRole)
    {
        this.cubeRole = cubeRole;


        switch (cubeRole)
        {
            case CubeRole.Wheel:
                DeactivateAllCameras();
                wheelCamera.enabled = true;
                break;
            case CubeRole.Sail:
                DeactivateAllCameras();
                sailCamera.enabled = true;
                break;
            case CubeRole.Map:
                print("Map cube role camera is not implemented yet");
                break;
        }
    }

}
