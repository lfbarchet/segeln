using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailController : MonoBehaviour
{
    // Level specifying to what extent the sail is hoisted
    public GameObject sail;
    public GameObject ship;
    [Range(0.0f, 1.0f)]
    public float sailOrientation = 0.5f;
    [Range(0.0f, 1.0f)]
    public float levelTarget = 0;
    public GameObject gaugeIndicator;
    [Range(0.0f, 1.0f)]
    public float gauge = 0;
    [Range(0.0f, 1.0f)]
    public float gaugeTarget = 0;
    //Delay between a target change in seconds
    public int targetChangeDelay = 3;
    public float speed = 0.0001f;
    public float updateSpeed = 1;
    public float sailMovementSpeed = 1;
    // Threshold in which the ship still maintains speed
    public float gaugeThreshold = 0.3f;
    public float currentShipSpeed = 0;

    private List<IEnumerator> coroutines;
    private float lastOrientation;
    public float currentOrientation;
    private Quaternion sailStartRotation;
    private Vector3 gaugeStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        coroutines = new List<IEnumerator>
        {
            NextGaugeTarget(targetChangeDelay),
            UpdateLevelTarget()
        };

        foreach (IEnumerator coroutine in coroutines)
            StartCoroutine(coroutine);

        sailStartRotation = sail.transform.localRotation;
        gaugeStartPosition = gaugeIndicator.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        gauge = CalculateGauge(speed);
        sailOrientation = FixOrientation(currentOrientation);

        sail.transform.localRotation = sailStartRotation * Quaternion.AngleAxis(sailOrientation, Vector3.forward);
        gaugeIndicator.transform.localPosition = new Vector3(gaugeStartPosition.x - 4 + 8 * gauge, gaugeStartPosition.y, gaugeStartPosition.z);

        CalculateCurrentSpeed();
    }

    void OnDestroy()
    {
        foreach (IEnumerator coroutine in coroutines)
            StopCoroutine(coroutine);
    }

    private void CalculateCurrentSpeed()
    {
        float s = (gauge * 360 - 180) * -1;
        float diff = Mathf.Abs(s - sailOrientation);
        Debug.LogWarning("Ga: " + gauge + " Sa: " + sailOrientation + " S: " + s + " Diff: " + diff);

        // publish the speed to the SailService
        SailService.Instance.HandleSailStateChangeFromLocal(new SailState
        {
            Speed = currentShipSpeed,
            Timestamp = System.DateTime.UtcNow
        });
    }

    private float CalculateGauge(float speed)
    {
        float newGauge = gauge;

        if (gauge < gaugeTarget)
            newGauge += speed;
        else if (gauge > gaugeTarget)
            newGauge -= speed;
        else
            newGauge = gaugeTarget;

        return newGauge;
    }

    private float FixOrientation(float orientation)
    {
        if (orientation > 180)
        {
            return orientation - 360;
        }
        else if (orientation < -180)
        {
            return orientation + 360;
        }
        else
        {
            return orientation;
        }
    }

    public void HandleOrientation(float orientation)
    {
        float degrees = orientation;
        currentOrientation = degrees;
    }

    private IEnumerator NextGaugeTarget(int delay)
    {
        while (true)
        {
            gaugeTarget = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator UpdateLevelTarget()
    {
        lastOrientation = currentOrientation;

        while (true)
        {
            float orientationDifference = lastOrientation - currentOrientation;

            if (lastOrientation < 100 && currentOrientation > 250)
            {
                orientationDifference = currentOrientation - lastOrientation + 360;
            }
            else if (lastOrientation > 250 && currentOrientation < 100)
            {
                orientationDifference = currentOrientation + 360 - lastOrientation;
            }

            orientationDifference /= 360;
            float newLevelTarget = levelTarget + orientationDifference;

            if (0 <= newLevelTarget && newLevelTarget <= 1)
                levelTarget = newLevelTarget;

            lastOrientation = currentOrientation;

            yield return new WaitForSeconds(updateSpeed);
        }
    }
}