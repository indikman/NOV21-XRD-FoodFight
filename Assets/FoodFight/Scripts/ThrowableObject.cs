using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject
{
    [SerializeField] private GrabType grabbingType;

    [SerializeField] private float throwBoost;
    [SerializeField] private int numOfVelocitySamples;

    private XRHand tempHand;
    private FixedJoint joint;
    private Vector3 previousPosition;
    private Queue<Vector3> previousVelocities = new Queue<Vector3>(); // Creates a blank queue


    public enum GrabType
    {
        Kinematic,
        JointBased
    }

    private void FixedUpdate()
    {
        var velocity = transform.position - previousPosition;
        previousPosition = transform.position;

        previousVelocities.Enqueue(velocity);

        if(previousVelocities.Count > numOfVelocitySamples)
        {
            previousVelocities.Dequeue();
        }

    }

    public override void OnGrabStart(XRHand hand)
    {
        tempHand = hand;

        if (grabbingType == GrabType.Kinematic)
        {
            base.OnGrabStart(hand); // Kinematic , child parent way
            
        }
        else if(grabbingType == GrabType.JointBased)
        {
            // joint way
            // add a new joint component to the grabbable
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = hand.GetComponent<Rigidbody>();
        }  
    }


    public override void OnGrabEnd()
    {
        if(grabbingType == GrabType.Kinematic)
        {
            base.OnGrabEnd();
        }else if (grabbingType == GrabType.JointBased) 
        {
            // remove the joint
            Destroy(joint);
        }


        // modifications!!
        // body.AddForce(throwBoost * tempHand.transform.forward); // shoot the objects

        Vector3 averageVelocity = Vector3.zero;

        foreach(var tempVelocity in previousVelocities)
        {
            averageVelocity += tempVelocity;  ///averageVelocity = averageVelocity + tempVelocity;
        }

        averageVelocity /=  previousVelocities.Count; // actual average velocity

        rb.velocity = averageVelocity * throwBoost;

    }

    
}
