using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance { get; private set; }

    [Header("Camera")]
    [SerializeField]
    private Camera sailCamera;
    [SerializeField]
    private Camera wheelCamera;
    [SerializeField]
    private Camera mapCamera;


    public TextMeshProUGUI fpsText;
    public float deltaTime;

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


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }


    public void DeactivateAllCameras()
    {
        mapCamera.enabled = false;
        sailCamera.enabled = false;
        wheelCamera.enabled = false;
    }

    public void ActivateCamera(CubeRole cubeRole)
    {
        print("CameraManager: " + cubeRole);

        if (wheelCamera == null) return;
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
                DeactivateAllCameras();
                mapCamera.enabled = true;
                break;
        }
    }





}
