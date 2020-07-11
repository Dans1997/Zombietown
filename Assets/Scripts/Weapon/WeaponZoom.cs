using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    private void OnDisable()
    {
        SetZoom(zoomedOutFOV, zoomedOutSensitivity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) 
            SetZoom(zoomedInFOV, zoomedInSensitivity);
        else 
            SetZoom(zoomedOutFOV, zoomedOutSensitivity);
    }

    private void SetZoom(float fov, float sensitivity)
    {
        GetComponentInParent<Camera>().fieldOfView = fov;
        GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.XSensitivity = sensitivity;
        GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.YSensitivity = sensitivity;
    }

}
