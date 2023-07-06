using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    int direction;
    float speed = 0.3f;
    GameObject player;

    public enum Forms{
        ball,
        walking
    }
    public Forms CurrentForm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim.SetBool("Spinning", true);
        direction = -1;
    }
    void Update()
    {
        if (CurrentForm == Forms.ball)
        {
        }
        else
        {
            anim.SetBool("Spinning", false);
            Walk();
        }
    }
    public void Walk()
    {
        if(direction < 0) { gameObject.transform.localScale = new Vector3(1, 1, 1);}
        else {gameObject.transform.localScale = new Vector3(-1, 1, 1);}
         rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentForm == Forms.ball)
        {
            CurrentForm = Forms.walking;
        }
        else
        {
           if (other.gameObject.tag == "Spiny")
           {
                if (direction == 1){direction = -1;}
                else{direction = 1;}
           }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Trigger"))
            {
                if (direction == 1){direction = -1;}
                else{direction = 1;}
            }
    }

}
