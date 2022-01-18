using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Color hoverColor;

    public MeshRenderer rend;
    private Color defaultColor;
    public Rigidbody rb;

    public Vector3 grabRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        rend.material.color = hoverColor;
    }

    public void OnHoverExit()
    {
        rend.material.color = defaultColor;
    }

    public virtual void OnGrabStart(XRHand hand)
    {
        transform.SetParent(hand.transform);
        transform.localEulerAngles += grabRotationOffset;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public virtual void OnGrabEnd()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public virtual void OnInteractionStart()
    {
        Debug.Log("Interaction started");
    }

    public virtual void OnInteractionUpdating()
    {

    }

    public virtual void OnInteractionStop()
    {

    }

}
