using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 65f;
    [SerializeField] float intensityAmount = 5f;

    private void OnTriggerEnter(Collider other)
    {
        FlashLight flashlight = FindObjectOfType<FlashLight>();
        flashlight.RestoreLightAngle();
        flashlight.AddLightIntensity(intensityAmount);
        Destroy(gameObject);
    }
}
