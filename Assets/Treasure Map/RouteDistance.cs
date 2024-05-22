using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteDistance : MonoBehaviour
{
    public Transform ship;

    public Transform path;
    public List<Transform> pathStrokes = new List<Transform>();

    public float smallestDistance;
    public GameObject nearestPathStroke;

    private void Start() {
        foreach (Transform child in path) {
            pathStrokes.Add(child);
        }
    }

    private void Update() {
        smallestDistance = float.MaxValue;
        nearestPathStroke = null;

        foreach (Transform stroke in pathStrokes) {
            float distance = Vector3.Distance(ship.position, stroke.position);
            if (distance < smallestDistance) {
                smallestDistance = distance;
                nearestPathStroke = stroke.gameObject;
            }
        }

        // vorübergehende Beispielausgaben, wenn man zu weit von der Route abweicht
        if (smallestDistance > 100) {
            Debug.Log("GameOver");
        }
        else if (smallestDistance > 50) {
            Debug.Log("Du hast dich zu weit von der Route entfernt. Drehe um!");
        }
       

        Debug.DrawLine(ship.position, nearestPathStroke.transform.position, Color.red);
    }
}
