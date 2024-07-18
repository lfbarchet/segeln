using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RouteDistance : MonoBehaviour
{
    public Transform ship;

    public Transform path;
    public List<Transform> pathStrokes = new List<Transform>();

    public float smallestDistance;
    public GameObject nearestPathStroke;

    public TextMeshProUGUI damageText;
    public float damage = 0;
    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            damageText.text = "Leben: " + (int) (1000-damage);
        }
    }



    public static RouteDistance Instance { get; private set; }

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

    private void Start()
    {
        foreach (Transform child in path)
        {
            pathStrokes.Add(child);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsMainRole())
        {

            smallestDistance = float.MaxValue;
            nearestPathStroke = null;

            foreach (Transform stroke in pathStrokes)
            {
                float distance = Vector3.Distance(ship.position, stroke.position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    nearestPathStroke = stroke.gameObject;
                }
            }
            Debug.DrawLine(ship.position, nearestPathStroke.transform.position, Color.red);
        }

        var deathValue = GameManager.Instance.IsMainRole() ? 1000 : 990;

        // vor�bergehende Beispielausgaben, wenn man zu weit von der Route abweicht
        if (Damage > deathValue)
        {
            GameManager.Instance.GameOver();
        }
        else if (smallestDistance > 100)
        {
            Debug.Log("Du hast dich zu weit von der Route entfernt. Drehe um!");
            Damage += 0.1f;
        }
    }
}
