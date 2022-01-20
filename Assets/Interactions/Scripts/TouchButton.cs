using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchButton : MonoBehaviour
{
    public Transform button;
    public Transform down;
    public AudioSource audioSource;
    public UnityEvent onButtonPressed;
    public UnityEvent onButtonReleased;

    private Vector3 originalPosition;

   
    void Start()
    {
        originalPosition = button.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.position = down.position;
            audioSource.Play();
            onButtonPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.position = originalPosition;
            onButtonReleased?.Invoke();
        }
    }
}
