using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowserController : MonoBehaviour
{
    public float JumpHeight;
    public Transform GroundHeight;
    float Delay = 0.5f;
    int direction;
    public float speed = 0.2f;
    public float projectileSpeed = 1.0f;
    Rigidbody2D rb;
    public GameObject Mario;
    bool AttackOnCooldown;
    bool JumpOnCooldown;
    [SerializeField] GameObject Fireball;
    int random;
    public int hitsLeft;// Hits from Marios superball that are left to defeat bowser
    GameObject gc;
    GameController gameCtrl;
    bool isALive;
    public enum WalkDirections
    {
        right,
        left
    }
    //Three types of bowsers that can be in the game
    WalkDirections NextDirection;
    public enum BowserForms
    {
        fire,//Only in levels 1-5
        hammer,//Only in levels 6-7
        both//Only in 8
    }
    public BowserForms CurrentForm;
    void Start()
    {
        isALive = true;
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        AttackOnCooldown = false;
        NextDirection = WalkDirections.right;
        direction = -1;
        JumpOnCooldown = false;
    }
    void Update()
    {   //Perform the attack if not on cooldown
        if(AttackOnCooldown == false)
        {
            AttackOnCooldown = true;
            FireBallAttack();
            StartCoroutine(AttackDelay(1.5f));
        }//Assign the next direction
        if(NextDirection == WalkDirections.left)
        {
            StartCoroutine(WalkForward());
        }
        if(NextDirection == WalkDirections.right)
        {
            StartCoroutine(WalkBackwards());
        }//If not on cooldown perform a jump
        if(!JumpOnCooldown)
        {
            StopCoroutine(WalkBackwards());
            StopCoroutine(WalkForward());
            StartCoroutine(Jump());
        } 
    }
    //Walk towards left for 0.5 of a second
    public IEnumerator WalkForward()
    {
        StopCoroutine(WalkBackwards());
        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);
        NextDirection = WalkDirections.right;      
        yield return null;                                                          
    }
    //Walk towards right for 0.5 of a second
    public IEnumerator WalkBackwards()
    {
        StopCoroutine(WalkForward());
        rb.velocity = new Vector2(1 * speed, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);
        NextDirection = WalkDirections.left;
        yield return null; 
    }

    public void FireBallAttack()
    {
        //Randomized attacks that can be spawned at 4 different position close to ground, slightly above ground, higher than mario, close to the same height as marios jump
        int random = RandomNumber(1,3);
        if (RandomNumber(1,3) == 1)
        {
            random = RandomNumber(1,3);
            if(random == 1){Instantiate(Fireball, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y -0.1f , 0.0f), Quaternion.identity);}
            else{Instantiate(Fireball, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y + 0.05f , 0.0f), Quaternion.identity);}
        }
        else
        {
            random = RandomNumber(1,3);
            if(random == 1){Instantiate(Fireball, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y - 0.05f , 0.0f), Quaternion.identity);}
            else{Instantiate(Fireball, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y + 0.1f , 0.0f), Quaternion.identity);}
        }
    }
    public int RandomNumber(int low, int high)
    {
        return Random.Range(low, high);
    }
    //Perform a jump with bowser
    public IEnumerator Jump()
    {
        JumpOnCooldown = true;
        rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        if(AttackOnCooldown == false)
        {
            AttackOnCooldown = true;
            FireBallAttack();
            StartCoroutine(AttackDelay(1.5f));
        }
        yield return new WaitForSeconds(3.0f);
        JumpOnCooldown = false;
    }
    //Flip the sprite
    public void SwitchDirection()
    {
        if (Mario.transform.position.x < gameObject.transform.position.x){direction = -1;}
        else{ direction = 1; }
    }
    //Delay in betweween attacks
    public IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AttackOnCooldown = false;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "FireBall")
        {
            hitsLeft--;
            if (hitsLeft == 0) { isALive = false; gameCtrl.incScore(500);}
        }
        if (other.gameObject.tag == "Player")
        {
            gameCtrl.loseOfLife();
        }
    }
}
