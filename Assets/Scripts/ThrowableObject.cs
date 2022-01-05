using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float throwBoost;

    private XRHand tempHand;

    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);

        tempHand = hand;
    }


    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        // modifications!!
        body.AddForce(throwBoost * tempHand.transform.forward);
    }
}
