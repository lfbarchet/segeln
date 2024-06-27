using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importieren des korrekten Namespace für TextMeshPro

public class TextDelay : MonoBehaviour
{
    private TextMeshProUGUI textContent; // Verwenden der richtigen Komponente
    private int count = 0;
    private bool inProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        textContent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inProgress)
        {
            StartCoroutine(waitingDot());
        }
    }

    IEnumerator waitingDot()
    {
        inProgress = true;
        yield return new WaitForSeconds(1);
        if(count == 3)
        {
            textContent.text = textContent.text.Substring(0,textContent.text.Length - 3);
            count = 0;
        }
        textContent.text += ".";
        count++;
        inProgress = false;
    }
}
