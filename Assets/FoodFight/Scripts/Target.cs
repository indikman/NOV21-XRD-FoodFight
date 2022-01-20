using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float moveSpeed, moveAmount, spinupAmount, upDownSpeed;
    public Lever lever;

    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        RandomMoveAmount(10);
        lever = FindObjectOfType<Lever>();
    }

    public void RandomMoveAmount(int MaxMove)
    {
        moveAmount = Random.Range(0, MaxMove);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * moveSpeed * lever.NormalizedJointAngle();

        var newPosition = transform.position;

        newPosition.x = startPosition.x + Mathf.Sin(Time.time * moveSpeed) *moveAmount ;
        newPosition.y = startPosition.y + Mathf.Sin(Time.time * upDownSpeed) * spinupAmount;

        transform.position = newPosition;
    }
}
