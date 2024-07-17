using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLightController : MonoBehaviour
{
    public Light light;
    private float intensityTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, intensityTarget, 0.01f);
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            intensityTarget = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        }
    }
}
