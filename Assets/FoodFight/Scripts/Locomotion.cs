using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    public Transform xrRig;
    public float playerSpeed = 2f;
    public float height = 2f;

    [Range(5, 40)]
    public int lineResolution = 10;
    public Renderer screen;

    private XRHand controller;
    private LineRenderer line;
    private Vector3 hitPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRHand>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.positionCount = lineResolution +1;
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
            // turnary operator
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
            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, hitInfo.point);

            hitPosition = hitInfo.point;

            // curve the line
            CurveLine(hitInfo.point);

            bool validTarget = hitInfo.collider.CompareTag("Teleporation");

            Color color = validTarget ? Color.blue : Color.red;

            line.endColor = color;
            line.startColor = color;

            if(validTarget && Input.GetButtonDown($"XRI_{controller.hand}_TriggerButton"))
            {
                StartCoroutine(FadeTeleport());
            }

        }
        else
        {
            line.enabled = false;
        }
    }


    void CurveLine(Vector3 hitPosition)
    {
        Vector3 A = controller.transform.position;
        Vector3 C = hitPosition;
        Vector3 B = (C - A) / 2 + A;

        B.y += height;

        for (int i = 0; i <= lineResolution; i++)
        {
            float t = (float)i / (float)lineResolution;
            Vector3 AtoB = Vector3.Lerp(A, B, t);
            Vector3 BtoC = Vector3.Lerp(B, C, t);
            Vector3 curvePosition = Vector3.Lerp(AtoB, BtoC, t);

            line.SetPosition(i, curvePosition);
        } 
    }

    private IEnumerator FadeTeleport()
    {
        float currentTime = 0f;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }
        xrRig.position = hitPosition;

        yield return new WaitForSeconds(0.5f);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

    }
}
