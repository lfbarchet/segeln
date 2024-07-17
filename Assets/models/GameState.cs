using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameState
{
    private float shipPositionX;
    private float shipPositionY;
    private float shipPositionZ;

    private float shipRotationX;
    private float shipRotationY;
    private float shipRotationZ;


    private float damage;

    private DateTime timestamp;

    public float ShipPositionX { get => shipPositionX; set => shipPositionX = value; }
    public float ShipPositionY { get => shipPositionY; set => shipPositionY = value; }
    public float ShipPositionZ { get => shipPositionZ; set => shipPositionZ = value; }

    public float ShipRotationX { get => shipRotationX; set => shipRotationX = value; }
    public float ShipRotationY { get => shipRotationY; set => shipRotationY = value; }
    public float ShipRotationZ { get => shipRotationZ; set => shipRotationZ = value; }


    public float Damage { get => damage; set => damage = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
}
