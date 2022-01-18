using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : ThrowableObject
{
    public GameObject paintPrefab;
    public Transform paintbrushTip;
    private GameObject spawnedPaint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        spawnedPaint = Instantiate(paintPrefab, paintbrushTip.position, paintbrushTip.rotation);


    }

    public override void OnInteractionUpdating()
    {
        if (spawnedPaint)
        {
            spawnedPaint.transform.position = paintbrushTip.position;
        }
    }

    public override void OnInteractionStop()
    {
        spawnedPaint.transform.position = spawnedPaint.transform.position;
    }
}
