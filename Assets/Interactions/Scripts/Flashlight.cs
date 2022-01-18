using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : GrabbableObject
{
    private Light flashlight;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponentInChildren<Light>();
    }

    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        flashlight.enabled = !flashlight.enabled;
    }
}
