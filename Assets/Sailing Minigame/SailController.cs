using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailController : MonoBehaviour
{
    // Level specifing to what extend the sail is hoisted
    [Range(0.0f, 1.0f)]
    public float level = 0;
    [Range(0.0f, 1.0f)]
    public float gauge = 0;
    [Range(0.0f, 1.0f)]
    public float gaugeTarget = 0;
    //Delay between a target change in seconds
    public int targetChangeDelay = 3;
    public float speed = 0.0001f;

    private IEnumerator coroutine;
    
    
    // Start is called before the first frame update
    void Start()
    {
        coroutine = NextGaugeTarget(targetChangeDelay);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        gauge = CalculateGauge(speed);
    }

    void OnDestroy()
    {
        StopCoroutine(coroutine);
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

    // Adjust the current sail level based on cube orientation
    public void HandleOrientation(float orientation)
    {
        float degrees = orientation + 180;
        level = degrees/360;
    }

    // Choose a new target
    private IEnumerator NextGaugeTarget(int delay)
    {
        while(true){
            gaugeTarget = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(delay);
        }
    }
}
