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

    bool zoomedInToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Adapt this to different types of zooms
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomedInToggle)
            {
                GetComponent<Camera>().fieldOfView = zoomedInFOV;
                GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.XSensitivity = zoomedInSensitivity;
                GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.YSensitivity = zoomedInSensitivity;
            }
            else
            {
                GetComponent<Camera>().fieldOfView = zoomedOutFOV;
                GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.XSensitivity = zoomedOutSensitivity;
                GetComponentInParent<RigidbodyFirstPersonController>().mouseLook.YSensitivity = zoomedOutSensitivity;
            }
            zoomedInToggle = !zoomedInToggle;
        }
    }
}
