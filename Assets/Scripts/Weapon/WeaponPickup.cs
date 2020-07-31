using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon unlockedWeapon;
    [SerializeField] int weaponCode;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>()) return;
        unlockedWeapon.UnlockWeapon();
        unlockedWeapon.GetComponentInParent<WeaponSwitcher>()?.SetCurrentWeapon(weaponCode);
        Destroy(gameObject);
    }
}
