using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeColliders : MonoBehaviour
{
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private bool detectionColliderDetected;

    private void OnTriggerEnter(Collider other)
    {
        colliderCount++;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }
}
