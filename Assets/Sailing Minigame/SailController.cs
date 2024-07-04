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
    public float sailMovementSpeed = 1;
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


    // // Start is called before the first frame update
    // void Start()
    // {
    //     coroutines = new List<IEnumerator>();
    //     coroutines.Add(NextGaugeTarget(targetChangeDelay));
    //     coroutines.Add(UpdateLevelTarget());

    //     foreach(IEnumerator coroutine in coroutines)
    //         StartCoroutine(coroutine);

    //     sailStartRotation = sail.transform.localRotation;
    //     gaugeStartPosition = gaugeIndicator.transform.localPosition;
    //     // sail.transform.rotation = 30;
    //     // lastLevel = level;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     gauge = CalculateGauge(speed);
    //     level = CalculateLevel(sailMovementSpeed);
    //     // gaugeIndicator.transform.position = new Vector3(-7, 2+(gauge*3), 0);
    //     // sail.transform.position = new Vector3(0, 4+(level*3), 0);
    //     sail.transform.localRotation = sailStartRotation * Quaternion.AngleAxis((sailStartRotation.y +30) - (60*level), Vector3.forward);
    //     gaugeIndicator.transform.localPosition = new Vector3((gaugeStartPosition.x - 4 + 8*gauge), gaugeStartPosition.y, gaugeStartPosition.z); // + (10*gauge), gaugeStartPosition.y, gaugeStartPosition.z);
    //     // gaugeIndicator.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);

    //     CalculateCurrentSpeed();
    // }

    // void OnDestroy()
    // {
    //     foreach(IEnumerator coroutine in coroutines)
    //         StopCoroutine(coroutine);
    // }

    // private void CalculateCurrentSpeed()
    // {
    //     float levelToGaugeDiff = Mathf.Abs(gauge-level);

    //     currentShipSpeed = 1 - (levelToGaugeDiff/gaugeThreshold);
    //     if(currentShipSpeed < 0)
    //         currentShipSpeed = 0;

    //     // publish the speed to the SailService
    //     SailService.Instance.HandleSailStateChangeFromLocal(new SailState
    //     {
    //         Speed = currentShipSpeed,
    //         Timestamp = System.DateTime.UtcNow
    //     });
    // }

    // // Move the current gauge level slowly towards the target level
    // private float CalculateGauge(float speed)
    // {
    //     float newGauge = gauge;

    //     if (gauge < gaugeTarget)
    //         newGauge += speed;
    //     else if (gauge > gaugeTarget)
    //         newGauge -= speed;
    //     else
    //         newGauge = gaugeTarget;


    //     return newGauge;
    // }

    // private float CalculateLevel(float speed)
    // {
    //     float newLevel = level;
    //     if(level < levelTarget)
    //         newLevel += speed;
    //     else if (level > levelTarget)
    //         newLevel -= speed;
    //     else
    //         newLevel = levelTarget;

    //     return newLevel;
    // }

    // Adjust the current sail level based on cube orientation
    public void HandleOrientation(float orientation)
    {
        float degrees = orientation + 180;
        // if (lastOrientation == null)
        //     lastOrientation = degrees;

        // if (Mathf.Abs(lastOrientation-degrees) < 2)
        // {
        // lastOrientation = lastOrientation > 250 ? 360 : 0;
        currentOrientation = degrees;
        // } else
        // lastOrientation = degrees;
        // }
        // level = lastOrientation/360;
    }

    // // Choose a new target
    // private IEnumerator NextGaugeTarget(int delay)
    // {
    //     while(true){
    //         gaugeTarget = Random.Range(0.0f, 1.0f);
    //         yield return new WaitForSeconds(delay);
    //     }
    // }

    // private IEnumerator UpdateLevelTarget()
    // {
    //     lastOrientation = currentOrientation;

    //     while(true)
    //     {
    //         float orientationDifference = lastOrientation - currentOrientation;

    //         // Prevent rotation reset, e.g. when cube goes from rot 290 to 30 in the given time frame
    //         if(lastOrientation < 100 && currentOrientation > 250)
    //         {
    //             orientationDifference = currentOrientation - (lastOrientation + 360);
    //         } else if(lastOrientation > 250 && currentOrientation < 100)
    //         {
    //             orientationDifference = (currentOrientation + 360) - lastOrientation;
    //         }

    //         // normalize orientationDifference
    //         orientationDifference = orientationDifference/360;
    //         float newLevelTarget = levelTarget + orientationDifference;

    //         // Clamp target Level between 0 and 1
    //         if(0 <= newLevelTarget && newLevelTarget <= 1)
    //             levelTarget = newLevelTarget;

    //         // Update last orientation
    //         lastOrientation = currentOrientation;

    //         yield return new WaitForSeconds(updateSpeed);
    //     }
    // }

    // // private IEnumerator UpdateLevel(int updateSpeed)
    // // {
    // //     while(true){
    // //         level = ;
    // //         yield return new WaitForSeconds(updateSpeed);
    // //     }
    // // }
}
