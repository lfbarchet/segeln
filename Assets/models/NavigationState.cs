using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Models;
using UnityEngine;
using UnityEngine.Events;



public class NavigationState
{
    private float orientation;
    private float speed;
    private Vector2 position;

    public float Orientation { get => orientation; set => orientation = value; }
    public float Speed { get => speed; set => speed = value; }
    public Vector2 Position { get => position; set => position = value; }

}
