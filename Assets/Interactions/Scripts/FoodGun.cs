using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGun : ThrowableObject
{
    public Rigidbody bulletprefab;
    public Transform spawnPoint;
    public float shootForce;

    
    void Start()
    {
        
    }

    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        Rigidbody bullet = Instantiate(bulletprefab, spawnPoint.position, spawnPoint.rotation);

        bullet.AddForce(bullet.transform.forward * shootForce);

        Destroy(bullet, 3f);

    }

}
