using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
   

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

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
