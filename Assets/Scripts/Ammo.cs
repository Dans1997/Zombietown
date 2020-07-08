using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoCount = 16;

    public int GetCurrentAmmo() { return ammoCount; }

    public void ReduceCurrentAmmo() { ammoCount--; }
}
