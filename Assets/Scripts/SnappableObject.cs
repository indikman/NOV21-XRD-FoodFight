using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : GrabbableObject
{
    [SerializeField] private Rigidbody body;

    private Transform CurrentSocket;

   
    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        if (CurrentSocket != null)
        {
            body.isKinematic = true;
            transform.position = CurrentSocket.position;
            transform.rotation = CurrentSocket.rotation;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            CurrentSocket = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            CurrentSocket = null;
        }
    }
}
