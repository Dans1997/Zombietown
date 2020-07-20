using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 16;

    [Header("Pickup SFX")]
    [SerializeField] AudioClip pickupSFX;

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, 0.4f);
        Destroy(gameObject);
    }
}
