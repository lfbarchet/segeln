using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailController : MonoBehaviour
{
    // Level specifing to what extend the sail is hoisted
    public GameObject sail;
    public GameObject ship;
    [Range(0.0f, 1.0f)]
    public float level = 0.5f;
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
    public float sailMovementSpeed = 0.0005f;
    // Threshold in which the ship still maintains speed
    public float gaugeThreshold = 0.3f;
    public float currentShipSpeed = 0;

    private List<IEnumerator> coroutines;
    private float lastGauge;
    private float lastOrientation;
    public float currentOrientation;
    private float lastLevelTarget;
    private Quaternion sailStartRotation;
    private Vector3 gaugeStartPosition;

    private readonly float DEATH_Y = -135;


    // Start is called before the first frame update
    void Start()
    {
        // Setup Start Parameters
        sailStartRotation = sail.transform.localRotation;
        gaugeStartPosition = gaugeIndicator.transform.localPosition;
        Debug.Log("Sail Start Rotation: " + sailStartRotation);

        // Setup Coroutines
        coroutines = new List<IEnumerator>();
        coroutines.Add(NextGaugeTarget(targetChangeDelay));
        coroutines.Add(UpdateLevelTarget());

        foreach (IEnumerator coroutine in coroutines)
            StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CubeRole != CubeRole.Sail || !GameManager.Instance.IsRunning)
            return;


        var deathYValue = GameManager.Instance.IsMainRole() ? DEATH_Y : DEATH_Y - 10;
        if (ship.transform.position.y < deathYValue)
        {
            GameManager.Instance.GameOver();
            return;
        }

        // Calculate current gauge and level values
        gauge = CalculateGauge(speed);
        level = CalculateLevel(sailMovementSpeed);

        // Move sail and gauge indicator based on gauge and level values
        sail.transform.localRotation = sailStartRotation * Quaternion.AngleAxis((sailStartRotation.y - 30) + (60 * level), Vector3.forward);
        gaugeIndicator.transform.localPosition = new Vector3((gaugeStartPosition.x + 4 - 8 * gauge), gaugeStartPosition.y, gaugeStartPosition.z);

        // Update ship speed
        CalculateCurrentSpeed();
    }

    // Stop all coroutines on destruction of this game object.
    void OnDestroy()
    {
        if (GameManager.Instance.CubeRole != CubeRole.Sail || !GameManager.Instance.IsRunning)
            return;

        foreach (IEnumerator coroutine in coroutines)
            StopCoroutine(coroutine);
    }

    /** 
        Calculate the current ship speed based on the alignemnt of level and gauge value.
        If the end of the sail points to the gauge indicator, the ship has its maximum speed of 1.
        Pointing away from the gauge indicator will continuously slow down the ship,
        leading to the ship coming to halt when pointing too far away from the indicator.
    */
    private void CalculateCurrentSpeed()
    {
        float levelToGaugeDiff = Mathf.Abs(gauge - level);

        currentShipSpeed = 1 - (levelToGaugeDiff / gaugeThreshold);
        if (currentShipSpeed < 0)
            currentShipSpeed = 0;

        // publish the speed to the SailService
        SailService.Instance.HandleSailStateChangeFromLocal(new SailState
        {
            Speed = currentShipSpeed,
            Timestamp = System.DateTime.UtcNow
        });
    }

    // Move the current gauge level slowly towards the target level
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

    /**
        Moves the sail closer to the level target.
        This function serves as a smoothing mechanism for the rotation of the cube.
    */
    private float CalculateLevel(float speed)
    {
        float newLevel = level;
        if (level < levelTarget)
            newLevel += speed;
        else if (level > levelTarget)
            newLevel -= speed;
        else
            newLevel = levelTarget;

        return newLevel;
    }

    // Adjust the current sail level based on cube orientation
    public void HandleOrientation(float orientation)
    {
        currentOrientation = orientation + 180;
    }

    // Choose a new gauge target
    private IEnumerator NextGaugeTarget(int delay)
    {
        while (true)
        {
            gaugeTarget = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(delay);
        }
    }

    //Update the level target based on the orientation difference within a time delta.
    private IEnumerator UpdateLevelTarget()
    {
        lastOrientation = currentOrientation;

        while (true)
        {
            float orientationDifference = lastOrientation - currentOrientation;

            // Prevent rotation reset, e.g. when cube goes from rot 290 to 30 in the given time frame
            if (lastOrientation < 100 && currentOrientation > 250)
            {
                orientationDifference = currentOrientation - (lastOrientation + 360);
            }
            else if (lastOrientation > 250 && currentOrientation < 100)
            {
                orientationDifference = (currentOrientation + 360) - lastOrientation;
            }

            // normalize orientationDifference
            orientationDifference = orientationDifference / 360;
            float newLevelTarget = levelTarget + orientationDifference;

            // Clamp target Level between 0 and 1
            if (0 <= newLevelTarget && newLevelTarget <= 1)
                levelTarget = newLevelTarget;

            // Update last orientation
            lastOrientation = currentOrientation;

            yield return new WaitForSeconds(updateSpeed);
        }
    }
}
