using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMover : MonoBehaviour
{
    public float moveDistance = 0.1f; // Distance to move back
    public float moveSpeed = 5f;      // Speed of the movement

    private Vector3 initialLocalPosition;  // Local starting position relative to the camera
    private bool isMovingBack = false;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial local position relative to the camera
        initialLocalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Detect left mouse button click (or touchpad click)
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MoveBackAndForth());
        }
    }

    IEnumerator MoveBackAndForth()
    {
        // Move the object back relative to its local forward direction
        Vector3 targetLocalPosition = initialLocalPosition - Vector3.forward * moveDistance;

        // Move back smoothly
        while (Vector3.Distance(transform.localPosition, targetLocalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Move forth (back to the initial local position)
        while (Vector3.Distance(transform.localPosition, initialLocalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialLocalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
