using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 0.1f;
    [SerializeField] float minimumAngle = 40f;

    float restoreAngle = 80f;
    float restoreIntensity = 10f;

    // Cached Components
    AudioSource audioSource;
    Light myLight = null;

    public void RestoreLightAngle() => myLight.spotAngle = restoreAngle;

    public void RestoreIntensity() => myLight.intensity = restoreIntensity;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        myLight = GetComponent<Light>();
        StartCoroutine(HandleLightFailures());
    }

    void FixedUpdate()
    {
        if (!myLight) return;
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle >= minimumAngle) myLight.spotAngle -= angleDecay;
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity >= 0) myLight.intensity -= lightDecay;
    }

    private IEnumerator HandleLightFailures()
    {
        while(true)
        {
            if (myLight.intensity < restoreIntensity / 2)
            {
                float currentIntensity = myLight.intensity;
                myLight.intensity = 0;
                yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.1f));
                myLight.intensity = currentIntensity;

                if(!audioSource.isPlaying) audioSource.Play();
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1.3f));
        }
    }
}
