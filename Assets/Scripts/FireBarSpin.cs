using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarSpin : MonoBehaviour
{
    public float spinSpeed = 90f; // The speed of the spin, in degrees per second
    public Transform spinAroundPoint; // The point to rotate around
    float rotationDirection = 1.0f;// Clockwise rotation of the bar

    void Update() {     
        SpinAroundTop();
    }

    void SpinAroundTop() {
        // Rotate the rectangle around the top of the rectangle
        transform.RotateAround(spinAroundPoint.position, Vector3.forward, spinSpeed * rotationDirection * Time.deltaTime);
    }
}
