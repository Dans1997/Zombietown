using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] Canvas damageCanvas;
    [SerializeField] float displayTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        damageCanvas.enabled = false;
    }

    public void ShowCanvas()
    {
        StartCoroutine(EnableCanvas());
    }

    IEnumerator EnableCanvas()
    {
        damageCanvas.enabled = true;
        yield return new WaitForSeconds(displayTime);
        damageCanvas.enabled = false;
    }
}
