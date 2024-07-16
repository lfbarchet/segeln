using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    [Header("Game Status")]
    [SerializeField]
    private bool isRunning = false;
    public bool IsRunning
    {
        get => isRunning; set
        {
            isRunning = value;
            if (value)
            {
                SetCubeRole(cubeRole);
            }
            else
            {
                CameraManager.Instance?.DeactivateAllCameras();
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {   
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("New scene loaded: " + scene.name);
        CameraManager.Instance?.ActivateCamera(cubeRole);
    }

    void Start()
    {
        SetCubeRole(cubeRole);
        IsRunning = false;
    }

    void OnValidate()
    {
        print("OnValidate: " + cubeRole);
        SetCubeRole(cubeRole);
        print("id:"+SystemInfo.graphicsDeviceVendorID);
    }

    public void SetCubeRole(CubeRole cubeRole)
    {
        //if (!IsRunning) return; 
        print("gamemanager: " + cubeRole);
        this.cubeRole = cubeRole;
        print("cameramanager: "+  CameraManager.Instance);
        CameraManager.Instance?.ActivateCamera(cubeRole);
    }

    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    public bool IsMainRole()
    {
        return cubeRole == CubeRole.Map;
    }

}
