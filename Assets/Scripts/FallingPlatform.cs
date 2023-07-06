using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    GameObject Player;
    float fallSpeed;
    public Rigidbody2D rb;
    void Start()
    {
        rb.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            rb.gravityScale = 0.1f;
        }
    }
}
