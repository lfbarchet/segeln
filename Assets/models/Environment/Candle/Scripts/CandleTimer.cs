using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleTimer : MonoBehaviour
{
    public int playTime;
    public List<Rigidbody> nails;

    private ConstantForce cForce;
    private Vector3 forceDirection;
    // Start is called before the first frame update
    void Start()
    {
        forceDirection = new Vector3(0, -150, 0);
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Timer()
    {
        foreach (Rigidbody nail in nails)
        {
            yield return new WaitForSeconds(playTime / nails.Count);
            nail.isKinematic = false;

            cForce = nail.GetComponent<ConstantForce>();
            cForce.force = forceDirection;
        }
        yield return new WaitForSeconds(3);


        GameManager.Instance.GameOver();
    }
}
