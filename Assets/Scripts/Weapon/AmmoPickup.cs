using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 16;

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}
