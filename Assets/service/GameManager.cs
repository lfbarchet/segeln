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

    public CubeRole cubeRole = CubeRole.Wheel;

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

}
