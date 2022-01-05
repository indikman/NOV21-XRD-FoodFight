using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Color hoverColor;

    private MeshRenderer rend;
    private Color defaultColor;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        defaultColor = rend.material.color;
        rb = GetComponent<Rigidbody>();
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
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public virtual void OnGrabEnd()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
