using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKoopaTroopa : MonoBehaviour
{
    public enum KoopaStates
    {
        air,
        ground,
        shell,
        jumping,
        movingShell
    }
    public KoopaStates KoopaState;
    public SpriteRenderer sr;
    public Sprite KoopaShell;
    public Animator anim;
    int direction;
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    public float speed = 0.1f;
    public float JumpHeight = 2.0f;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    public bool isAlive;
    bool JumpOnCooldown;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        direction = -1;
        isAlive = true;
        boxCollider = GetComponent<BoxCollider2D>();
        JumpOnCooldown = false;
    }
    void Update()
    {
            //If koopa is in the air move it up and down
            if (KoopaState == KoopaStates.air)
            {
                rb.isKinematic = true;
                anim.SetBool("Flying", true);
                //Switch inbetween the two waypoints
                if(Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .2f)
                {
                    currentWaypoint++;
                    if(currentWaypoint >= waypoints.Length) {currentWaypoint = 0;}
                }
                //Move to the current waypoint
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
            }
            //If it is a jumping koopa move it slightly up and forwared
            else if (KoopaState == KoopaStates.jumping)
            {
                anim.SetBool("Flying", true);
                //
                if(  JumpOnCooldown == true )
                {
                    //If player is close enough make the Koopa start moving
                    if (Mathf.Abs( player.transform.position.x - gameObject.transform.position.x)<= 3.0f)
                    {   
                        rb.velocity = new Vector2(direction * speed * 3, rb.velocity.y);
                    }//Switch directions
                    if(direction < 0) { gameObject.transform.localScale = new Vector3(1, 1, 1);}
                    else {gameObject.transform.localScale = new Vector3(-1, 1, 1);} 
                }
                //If cooldown is down start the jump IEnumerator
                else if(JumpOnCooldown == false)
                {
                    StartCoroutine(Jump());
                }
            }
            //Else if koopa drops on the ground or it is ground koopa move it ward
            else if (KoopaState == KoopaStates.ground)
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
            else if (KoopaState == KoopaStates.shell)
            {
                gameObject.GetComponent<Animator>().enabled = false;
                sr.sprite = KoopaShell;
                if (speed == 1.1f) { speed = 1.1f;}
                else { speed = 0.0f; }
                rb.velocity = new Vector2(direction * speed, rb.velocity.y);
                boxCollider.size = new Vector2(0.1137037f, 0.06583888f);
                boxCollider.offset = new Vector2(0.007565975f, 0.01749301f);
            } 
    }
    //Perform the small jump for the Jumping koopas
     public IEnumerator Jump()
    {
        JumpOnCooldown = true;
        rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        yield return new WaitForSeconds(1.2f);
        JumpOnCooldown = false;
    }
    //Check for collisions depending on the state
    public void OnCollisionEnter2D(Collision2D other)
    {
        //If any form is hit by a fireball
        if(other.gameObject.CompareTag("Fireball"))
        {
            transform.rotation =  Quaternion.Euler(0, 0, 180);
            StartCoroutine(destroyEnemeyAfterDelay(0.3f, other.gameObject));
        }
        //If collision happens when in the air state
        if(KoopaState == KoopaStates.air)
        {
            //Check if the player lands on the Koopa from above
           if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down) && KoopaState == KoopaStates.air)
            {
                KoopaState = KoopaStates.ground;
            } 
        }
        //If the collision happens in the jumping state
        else if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down) && KoopaState == KoopaStates.jumping)
        {
            KoopaState = KoopaStates.ground;
        }
        //If collision of the ground koopa happens
        else if (KoopaState == KoopaStates.ground)
        {
            if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down) && KoopaState == KoopaStates.ground)
            {
                KoopaState = KoopaStates.shell;
            } 
        }
        //If Mario jumps on a koopa in its shell
        else if (other.gameObject.CompareTag("Player") && other.transform.DotTest(transform, Vector2.down) && KoopaState == KoopaStates.shell)
        {
            //Move it to the right
            if ( (KoopaState == KoopaStates.shell && other.transform.DotTest(transform, Vector2.down) && other.transform.position.x < transform.position.x) ||
            (KoopaState == KoopaStates.shell&& other.transform.position.x < transform.position.x ) )
            {
                speed = 1.1f;
                direction = 1;
            }//Move it to the left
            else if ( (KoopaState == KoopaStates.shell && other.transform.DotTest(transform, Vector2.down) && other.transform.position.x > transform.position.x) ||
            (KoopaState == KoopaStates.shell && other.transform.position.x > transform.position.x ) )
            {
                speed = 1.1f;
                direction = -1;
            }
        }
        
        
        //If it is a jumping or aground koopa collisions switch direction
        if (KoopaState ==KoopaStates.jumping || KoopaState ==KoopaStates.ground)
        {
            if (other.gameObject.tag == "Pipe" || other.gameObject.tag == "LittleGooba" || other.gameObject.tag == "KoopaTroopa" 
            || other.gameObject.tag == "Spring" || other.gameObject.tag == "Beetle" )
            {
                if (direction == -1){direction = 1;} else{direction = -1;}
            }
        }
        //If it is koopashell(dead) kill enemies and switch direction
        else
        {
           if (other.gameObject.tag == "Pipe" 
            || other.gameObject.tag == "Spring" )
            {
                if (direction == -1){direction = 1;} else{direction = -1;}
            } 

            if(other.gameObject.tag == "KoopaTroopa" || other.gameObject.tag == "LittleGooba" || other.gameObject.tag == "Beetle")
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
