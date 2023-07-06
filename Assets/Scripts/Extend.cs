using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend : MonoBehaviour
{
   
    public float speed = 1.0f;
    public float maxScale = 24.0f;
    public float minScale = 4.2f;
    private bool growing = true;
    private bool shrinking = true;
    public GameObject Platfrom1;
    public GameObject Platfrom2;
    public GameObject Rope1;
    public GameObject Rope2;
    float acceleration = 0.01f;
    GameObject player;
    bool collisionOccured = false;
    bool triggerOccured = false;
    public BoxCollider2D collider;
    public BoxCollider2D trigger;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update ()
    {
        // if (!stop)
        // {
        //     //Extend the left contract the right
        //     if (left)
        //     {
                // Grow(Platfrom1,Rope1);
                // Shrink(Platfrom2, Rope2);
        //     }
        //     //Check if extending on one side has finished 
        //     if (!growing && !shrinking)
        //     {
        //         if (!left)
        //         {
        //             stop = true;
        //         }
        //         else
        //         {
        //             left = false;
        //             growing = true;
        //             shrinking = true;
        //         }
        //     }

        //     if (!left)
        //     {
                // Grow(Platfrom2,Rope2);
                // Shrink(Platfrom1, Rope1);
        //     }
        // }
        if (collisionOccured && growing && shrinking)
        {
            Grow(Platfrom1,Rope1);
            Shrink(Platfrom2, Rope2);  
        }
        else
        {
            Rope1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Rope2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //speed = 1.0f;
        }
        if (triggerOccured && growing && shrinking)
        {
            Grow(Platfrom2,Rope2);
            Shrink(Platfrom1, Rope1); 
        }
        else
        {
            Rope1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Rope2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //speed = 1.0f;
        }
    }
    public void Grow(GameObject Platform, GameObject Rope)
    {
        if (growing) {
            // Increase the scale of the object
            speed += acceleration;
            Rope.transform.localScale += new Vector3(0, speed * 2 * Time.deltaTime, 0);
            Rope.GetComponent<Rigidbody2D>().velocity = new Vector2(Rope.GetComponent<Rigidbody2D>().velocity.x, (-speed / 10));
            //Moving the platform without the rigidbody
            Vector3 newPosition = Platform.transform.position;
            newPosition.y += (-speed / 13.0f) * Time.deltaTime;
            Platform.transform.position = newPosition;
            //Platform.GetComponent<Rigidbody2D>().velocity = new Vector2(Rope.GetComponent<Rigidbody2D>().velocity.x, (-speed / 10));
            if (Rope.transform.localScale.y >= maxScale) {
                growing = false;
                shrinking = false;
                collisionOccured = false;
                triggerOccured = false;
            }
        }
        
    }
    public void Shrink(GameObject Platform, GameObject Rope)
    {
        if (shrinking) {
            speed += acceleration;
            // Increase the scale of the object
            Rope.transform.localScale -= new Vector3(0, speed * 2 * Time.deltaTime, 0);
            Rope.GetComponent<Rigidbody2D>().velocity = new Vector2(Rope.GetComponent<Rigidbody2D>().velocity.x, (speed / 10));
            //Move the platform
            Vector3 newPosition = Platform.transform.position;
            newPosition.y += (speed / 13.0f) * Time.deltaTime;
            Platform.transform.position = newPosition;
            //Platform.GetComponent<Rigidbody2D>().velocity = new Vector2(Rope.GetComponent<Rigidbody2D>().velocity.x, (speed / 10));
            if (Rope.transform.localScale.y <= minScale) {
                shrinking = false;
                growing = false;
                collisionOccured = false;
                triggerOccured = false;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            growing = true;
            shrinking = true;
            UnityEngine.Debug.Log("Collision Occured");
            collisionOccured = true;

            Vector2 colliderOffset = collider.offset;
            colliderOffset.y = -0.37f;
            collider.offset = colliderOffset;
            Vector2 triggerOffset = trigger.offset;
            triggerOffset.y = 0.2f;
            trigger.offset = triggerOffset;
        }
    }
    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            growing = true;
            shrinking = true;
            UnityEngine.Debug.Log("Trigger Occured");
            triggerOccured = true;
            //Move collider andtrigger boundaries
            Vector2 triggerOffset = trigger.offset;
            triggerOffset.y = 0.3f;
            trigger.offset = triggerOffset;
            Vector2 colliderOffset = collider.offset;
            colliderOffset.y = 0.186685f;
            collider.offset = colliderOffset;
        }
    }
    // OFSET TRIGGER 0.3 before offeset(reset) -0.2315943 ;offset coliider -0.37; before offest(reset) 0.186685
}
