using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] bool isXEnabled = false, isYEnabled = false, isZEnabled = false;
    [SerializeField] float xSpeed = 0f, ySpeed = 0f, zSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateX();
        RotateY();
        RotateZ();
    }

    private void RotateX()
    {
        if (isXEnabled) 
        {
            transform.Rotate(Time.deltaTime * xSpeed, 0, 0, Space.Self);
        }
    }

    private void RotateY()
    {
        if (isYEnabled)
        {
            transform.Rotate(0, Time.deltaTime * ySpeed, 0, Space.Self);
        }
    }

    private void RotateZ()
    {
        if (isZEnabled)
        {
            transform.Rotate(0, 0, Time.deltaTime * zSpeed, Space.Self);
        }
    }
}
