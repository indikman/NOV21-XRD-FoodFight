using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : ThrowableObject
{
    public Vector3 centerOfMass;

    
    private HingeJoint hinge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;

        hinge = GetComponent<HingeJoint>();
    }

    public float NormalizedJointAngle()
    {
        float normalizedAngle = hinge.angle / hinge.limits.max;
        return normalizedAngle;
    }
}
