using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float moveSpeed, moveAmount, spinupAmount, upDownSpeed;

    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;

        newPosition.x = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        newPosition.y = startPosition.y + Mathf.Sin(Time.time * upDownSpeed) * spinupAmount;

        transform.position = newPosition;
    }
}
