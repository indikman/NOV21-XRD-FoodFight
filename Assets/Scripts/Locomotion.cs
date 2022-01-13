using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    public Transform xrRig;
    public float playerSpeed = 2f;
    private XRHand controller;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRHand>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleRaycast();
    }


    /// <summary>
    /// Handling snap turns
    /// </summary>
    private void HandleRotation()
    {
        // checking if the thumbstick is pressed
        if (Input.GetButtonDown($"XRI_{controller.hand}_Primary2DAxisClick"))
        {
            float rotation = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal") > 0 ? 30:-30 ;

            xrRig.Rotate(0, rotation, 0);

            //if(direction > 0)
            //{
            //    // rotate right
            //    xrRig.Rotate(0, 30, 0);
            //}
            //else
            //{
            //    xrRig.Rotate(0, -30, 0);
            //}

            // turnary operator

        }
    }

    /// <summary>
    /// Handle smooth motion forwards and sideways
    /// </summary>
    private void HandleMovement()
    {
        Vector3 forwardDirection = new Vector3(xrRig.forward.x, 0, xrRig.forward.z);
        Vector3 rightDirection = new Vector3(xrRig.right.x, 0, xrRig.right.z);

        forwardDirection.Normalize();
        rightDirection.Normalize();

        float horizontal = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal");
        float vertical = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Vertical");

        // move forwards and backwards
        xrRig.position += (vertical * Time.deltaTime * -forwardDirection * playerSpeed);

        // move left and right
        xrRig.position += (horizontal * Time.deltaTime * rightDirection * playerSpeed);


    }

    /// <summary>
    /// Handling raycast for teleporation
    /// </summary>
    void HandleRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hitInfo.point);
        }
        else
        {
            line.enabled = false;
        }
    }
}
