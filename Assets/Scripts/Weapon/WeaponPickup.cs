using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon unlockedWeapon;
    [SerializeField] int weaponCode;

    [Header("Pickup SFX")]
    [SerializeField] AudioClip pickupSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>()) return;
        unlockedWeapon.UnlockWeapon();
        unlockedWeapon.GetComponentInParent<WeaponSwitcher>()?.SetCurrentWeapon(weaponCode);
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position, 0.2f);
        Destroy(gameObject);
    }
}
