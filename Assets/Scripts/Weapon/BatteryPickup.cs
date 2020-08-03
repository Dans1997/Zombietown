using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 65f;
    [SerializeField] float intensityAmount = 5f;

    [Header("Pickup SFX")]
    [SerializeField] AudioClip pickupSFX;

    private void OnTriggerEnter(Collider other)
    {
        FlashLight flashlight = FindObjectOfType<FlashLight>();
        flashlight.RestoreLightAngle();
        flashlight.RestoreIntensity();
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, 0.5f);
        Destroy(gameObject);
    }
}
