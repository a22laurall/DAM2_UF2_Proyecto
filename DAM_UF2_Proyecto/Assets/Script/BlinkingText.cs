using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textElement; 
    public float blinkSpeed = 0.5f; 

    void Start()
    {
        if (textElement != null)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            textElement.enabled = !textElement.enabled; 
            yield return new WaitForSeconds(blinkSpeed); 
        }
    }

} 