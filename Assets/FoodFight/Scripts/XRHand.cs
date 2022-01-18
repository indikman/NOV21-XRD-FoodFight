using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    private GrabbableObject hoveredObject;
    private GrabbableObject grabbedObject;

    [SerializeField] private string grabButton;
    [SerializeField] private string triggerButton;

    public Animator anim;
    public Hand hand = Hand.Left;

    // Start is called before the first frame update
    void Start()
    {
        grabButton = $"XRI_{hand}_GripButton";
        triggerButton = $"XRI_{hand}_TriggerButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(grabButton))
        {
            // Grab
            if(hoveredObject != null)
            {
                grabbedObject = hoveredObject;
                hoveredObject = null;
                grabbedObject.OnGrabStart(this);
            }

            anim.SetBool("Gripped", true);
        }

        if(Input.GetButtonUp(grabButton))
        {
            // Release
            if(grabbedObject != null)
            {
                grabbedObject.OnGrabEnd();
                grabbedObject = null;
            }

            anim.SetBool("Gripped", false);
        }

        // start interaction
        if (Input.GetButtonDown(triggerButton) && grabbedObject)
        {
            grabbedObject.OnInteractionStart();
        }
        // update interaction
        if(Input.GetButton(triggerButton) && grabbedObject)
        {
            grabbedObject.OnInteractionUpdating();
        }
        // end interaction
        if (Input.GetButtonUp(triggerButton) && grabbedObject)
        {
            grabbedObject.OnInteractionStop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check whether it tiggeres with an object that we can grab
        GrabbableObject tempObject = other.GetComponent<GrabbableObject>();

        if (tempObject != null)
        {
            hoveredObject = tempObject;
            hoveredObject.OnHoverEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check whether hand is getting away from an object that we can grab
        GrabbableObject tempObject = other.GetComponent<GrabbableObject>();

        if (tempObject != null && hoveredObject != null)
        {
            hoveredObject.OnHoverExit();
            hoveredObject = null;
        }
    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}
