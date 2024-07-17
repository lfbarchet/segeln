using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleTimer : MonoBehaviour
{
    public int playTime;
    public List<Rigidbody> nails;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Timer()
    {
        foreach(Rigidbody nail in nails)
        {
            yield return new WaitForSeconds(playTime/nails.Count);
            nail.isKinematic = false;
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
        // TO DO: Call Game Over
    }
}
