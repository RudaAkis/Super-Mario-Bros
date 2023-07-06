using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public Sprite shellSprite;
    public SpriteRenderer sr;
    public Animator anim;
    float speed;
    GameObject player;
    public Rigidbody2D rb;
    int direction;
    public BoxCollider2D boxCollider;
    enum BeetleStates{
        walking,
        shell
    }
    BeetleStates BeetleState;
    void Start()
    {
        BeetleState = BeetleStates.walking;
        player = GameObject.FindGameObjectWithTag("Player");
        direction = -1;
        anim.SetBool("walking", true);
    }

    
    void Update()
    {
        if (BeetleState == BeetleStates.walking)
        {
            speed = 0.3f;
            rb.isKinematic = false;
            anim.SetBool("Flying", false);
            if (Mathf.Abs( player.transform.position.x - gameObject.transform.position.x ) <= 3.0f)
            {
                rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            }
            if(direction < 0) { gameObject.transform.localScale = new Vector3(1, 1, 1);}
            else {gameObject.transform.localScale = new Vector3(-1, 1, 1);}
        }
        else if (BeetleState == BeetleStates.shell)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            if (speed == 0.9f) { speed = 0.9f;}
            else { speed = 0.0f; }
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }    
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (BeetleState == BeetleStates.walking)
        {
            if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down))
            {
                BeetleState = BeetleStates.shell;
                anim.SetBool("walking", false);
                sr.sprite = shellSprite;
                speed = 0.0f;
            } 
        }
        //If Mario jumps on a koopa in its shell
        else if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down) &&  BeetleState == BeetleStates.shell)
        {
            //Move it to the right
            if ( other.transform.position.x < transform.position.x) 
            {
                speed = 0.9f;
                direction = 1;
            }//Move it to the left
            else if ( other.transform.position.x > transform.position.x ) 
            {
                speed = 0.9f;
                direction = -1;
            }
        }

        if (BeetleState == BeetleStates.walking)
        {
            if (other.gameObject.tag == "Pipe" || other.gameObject.tag == "LittleGooba" || other.gameObject.tag == "KoopaTroopa" 
            || other.gameObject.tag == "Spring" || other.gameObject.tag == "Beetle" )
            {
                if (direction == -1){direction = 1;} else{direction = -1;}
            }
        }
        
        else
        {
           if (other.gameObject.tag == "Pipe" 
            || other.gameObject.tag == "Spring" )
            {
                if (direction == -1){direction = 1;} else{direction = -1;}
            } 

            if(other.gameObject.tag == "KoopaTroopa" || other.gameObject.tag == "LittleGooba")
            {
                other.transform.rotation =  Quaternion.Euler(0, 0, 180);
                StartCoroutine(destroyEnemeyAfterDelay(0.3f, other.gameObject));
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
    IEnumerator destroyEnemeyAfterDelay( float delay, GameObject other)
    {
        other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.151f, other.transform.position.z);
        yield return new WaitForSeconds( delay ) ;
        Destroy(other);
    }
}
