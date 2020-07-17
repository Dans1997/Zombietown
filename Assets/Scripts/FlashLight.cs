using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 0.1f;
    [SerializeField] float minimumAngle = 40f;

    Light myLight = null;
    float restoreAngle = 80f;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        if (!myLight) return;
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle() { myLight.spotAngle = restoreAngle; }

    public void AddLightIntensity(float intensityAmount) { myLight.intensity += intensityAmount; }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle >= minimumAngle) myLight.spotAngle -= angleDecay;
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity >= 0) myLight.intensity -= lightDecay;
    }
}
