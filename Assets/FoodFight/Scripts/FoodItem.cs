using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
    private Vector3 spawnPos;
    private Quaternion spawnRot;

    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);

        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        GameManager.Instance.SpawnRandomFood(spawnPos, spawnRot);

        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
        
    }
}
