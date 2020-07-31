using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Win State")]
    [SerializeField] Canvas winCanvas;

    [Header("Base")]
    [Tooltip("Time the player needs to survive before returning to base.")]
    [SerializeField] int maxTime = 300;
    [SerializeField] Canvas enableBaseCanvas;

    bool hasEnabledBase = false;


    // Start is called before the first frame update
    private void Start()
    {
        enableBaseCanvas.enabled = false;
        winCanvas.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void HandleBaseActivation(int timeElapsed)
    {
        if (timeElapsed < maxTime || hasEnabledBase) return;
        hasEnabledBase = true;
        GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(ShowCanvas(enableBaseCanvas, 5f));
    }

    IEnumerator ShowCanvas(Canvas canvas, float canvasTime)
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(canvasTime);
        canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>()) return;
        winCanvas.enabled = true;
        Time.timeScale = 0;
    }
}
