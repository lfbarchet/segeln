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
    private float damage = 0;
    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            damageText.text = "Damage: " + damage;
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

        // vor�bergehende Beispielausgaben, wenn man zu weit von der Route abweicht
        if (Damage > 10000)
        {
            SceneManager.LoadScene(1);
        }
        else if (smallestDistance > 100)
        {
            Debug.Log("Du hast dich zu weit von der Route entfernt. Drehe um!");
            Damage += 0.1f;
        }


        Debug.DrawLine(ship.position, nearestPathStroke.transform.position, Color.red);
    }

}
