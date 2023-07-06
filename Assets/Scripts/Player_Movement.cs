using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float acceleration = 0.001f; // The acceleration of the player character
    public float speed = 0.028f; // The maximum speed of the player character
    public float currentSpeed = 0.0f; // The current speed of the player character
    bool grounded;// To check if the player is colliding with the ground
    public float jumpForce = 3.7f;// The force with which the player is pushed in the y direction
    public int direction;//left -1; right 1
    float regularSpeed, crouchingSpeed;
    public SpriteRenderer smallMario, superMario, fieryMarioRenderer;
    public GameObject Fireball;
    Rigidbody2D rigidbody;
    public Animator animator, superMarioAnimator,  fieryMarioAnimator;

    GameObject sMario,  fieryMario;
    GameObject gc;
    GameController gameCtrl;
    public float horizontalInput;
    public float last_x_position;
    int lastDir;
    public Transform beanStem;
    void Start()
    {
        regularSpeed = speed;
        crouchingSpeed = speed * 0.1f;

        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();

        rigidbody = GetComponent<Rigidbody2D>();
        //Assigning the super mario and Fiery Mario objects and their animators
        sMario = GameObject.FindGameObjectWithTag("SuperMario");
        superMarioAnimator = sMario.GetComponent<Animator>();
        fieryMario = GameObject.FindGameObjectWithTag("FieryMario");
        fieryMarioAnimator = fieryMario.GetComponent<Animator>();
        //Initial Collider size and offset
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.07316589f, 0.1191856f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.001773357f, -0.02035207f);

        gameCtrl.StageStart();
    }

    void Update()
    {
        /****Movement****/
        horizontalInput = Input.GetAxis("Horizontal");
        //Setting animator values
        setFloatANimValues();
        //Set direction and sprite direction
        setDiections();
        //Move Player
        MovePlayer();
        /***********************************************************************************/
        /****Crouching****/
        if(Input.GetKey(KeyCode.S)) { CheckCrouching(); }
        else
        {
            fieryMarioAnimator.SetBool("Crouching", false); 
            superMarioAnimator.SetBool("Crouching", false);
        }
        /**********************************************************************************/
        /****Shooting Fireballs****/
        if ( ( Input.GetKeyDown(KeyCode.LeftControl) && fieryMarioRenderer.enabled == true )  )
        {      
            shootFireball();
        }
        /**********************************************************************************/
    }
    public void PlayerJump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        animator.SetBool("Jumping", true);
        superMarioAnimator.SetBool("Jumping", true);
        fieryMarioAnimator.SetBool("Jumping", true); 
        StartCoroutine(jump(0.6f));
    }
    void MovePlayer()
    {
        //Regulating that the speed is not greater than the maximum speed to either direction
        if (currentSpeed >= 0.028f)
        {
            currentSpeed = 0.028f;
        }
        if (currentSpeed <= -0.028f)
        {
            currentSpeed = -0.028f;
        }
        if(direction == -1)
        {   
            if(lastDir == 0 || lastDir == 1 ){StartCoroutine(TurnAround()) ;}
            rigidbody.velocity = new Vector2(horizontalInput * -currentSpeed * 50, rigidbody.velocity.y);
        }
        else
        {
            if(lastDir == -1 || lastDir == 0){StartCoroutine(TurnAround()) ;}
            rigidbody.velocity = new Vector2(horizontalInput * currentSpeed * 50, rigidbody.velocity.y);
            last_x_position = gameObject.transform.position.x;
        }
        //Circle cast ground check method
        grounded = rigidbody.Raycast(Vector2.down);
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            PlayerJump();
        }
    }

    void setDiections()
    {
        if (horizontalInput < 0)
        {
            direction = -1;
            lastDir = direction;

            currentSpeed -= acceleration;
            //Flip the sprite
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
        else if (horizontalInput > 0)
        {
            direction = 1;
            lastDir = direction;
            currentSpeed += acceleration;
            //Flip the sprite
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            direction = 0;
            currentSpeed = 0;
        }
    }

    void setFloatANimValues()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput) );
        superMarioAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput) );
        fieryMarioAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput) );
    }
    void shootFireball()
    {
        if ( direction > 0 )
        {
            Instantiate(Fireball, new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y + 0.1f , 0.0f), Quaternion.identity);
        }
         if (direction < 0)
        {
            direction = -1;
            Instantiate(Fireball, new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y + 0.01f, 0.0f), Quaternion.identity);
        }
    }
    
    void CheckCrouching()
    {
        if ( (Input.GetKey(KeyCode.S) && superMario.enabled == true) )
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.09919f, 0.1268861f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.00177f, 0.003673233f);
            superMarioAnimator.SetBool("Crouching", true);
            speed = crouchingSpeed;
        }
        else if ( ( Input.GetKey(KeyCode.S) && fieryMarioRenderer.enabled == true )  )
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.09919f, 0.1268861f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.00177f, 0.003673233f);
            fieryMarioAnimator.SetBool("Crouching", true); 
            superMarioAnimator.SetBool("Crouching", false);
            speed = crouchingSpeed;
        }
        else
        {
            fieryMarioAnimator.SetBool("Crouching", false); 
            superMarioAnimator.SetBool("Crouching", false);
            speed = 0;
        }
        if(smallMario.enabled == true)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.07316589f, 0.1191856f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.001773357f, -0.02035207f);
            superMarioAnimator.SetBool("Crouching", false); 
            speed = regularSpeed;
        }
    }
    
    //Check Enemy collisions
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("LittleGooba") || other.gameObject.CompareTag("KoopaTroopa") || other.gameObject.tag == "Beetle" ||other.gameObject.tag == "BowserFire" 
        || other.gameObject.tag == "Bowser" || other.gameObject.tag == "LavaBall"
        || other.gameObject.tag == "Spiny" || other.gameObject.tag == "Squid" ) 
        {
            //Jump up for half of the jump force if Mario lands on the enemy from above 
            if (transform.DotTest(other.transform, Vector2.down))
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce / 2f);
                //If it is a goomba destroy it after a delay
                if ( other.gameObject.CompareTag("LittleGooba") )
                {
                    StartCoroutine(destroyEnemeyAfterDelay(0.3f, other.gameObject));
                } 
            }
            else
            {
                if (fieryMario.GetComponent<SpriteRenderer>().enabled == true)
                {
                    gameCtrl.SuperMario();
                }
                if (superMario.GetComponent<SpriteRenderer>().enabled == true)
                {
                    gameCtrl.SmallMario();
                }
                else
                {
                    gameCtrl.loseOfLife();
                }
            }
        }
    }
    //Check boss level enemy triggers
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BowserFire")
        {
            destroyEnemeyAfterDelay(0.0f, other.gameObject);
            gameCtrl.loseOfLife();
        }
        if (other.gameObject.tag == "LavaBall") {
                if (fieryMario.GetComponent<SpriteRenderer>().enabled == true)
                {
                    gameCtrl.SuperMario();
                }
                if (superMario.GetComponent<SpriteRenderer>().enabled == true)
                {
                    gameCtrl.SmallMario();
                }
                else
                {
                    gameCtrl.loseOfLife();
                } 
        }
    }
    
    void OnCollisionStay2D(Collision2D other)
    {
        if ( (other.gameObject.tag == "Spring") )
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce * 1.5f);
        }
        //Climb the beam stem
        if (other.gameObject.tag == "BeanStem")
        {
            animator.SetBool("LevelFinish", true);
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            transform.position += Vector3.up * 0.1f * Time.deltaTime;
            StartCoroutine(ClimbBeanStem(0.5f));
        }
    }
    //Jump off the climpoing bean stem
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "BeanStem")
        {
            animator.SetBool("LevelFinish", false);
            gameObject.transform.SetParent(null);
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }
    //Destroy a given object
    IEnumerator destroyEnemeyAfterDelay( float delay, GameObject enemy)
    {
        yield return new WaitForSeconds( delay ) ;
        Destroy(enemy);
    }
    IEnumerator ClimbBeanStem( float delay)
    {
        yield return new WaitForSeconds( delay ) ;
        gameObject.transform.position = beanStem.position;
        animator.SetBool("Climb", true);
        animator.SetBool("LevelFinish", false);
        yield return new WaitForSeconds( 1.5f) ;
        animator.SetBool("Climb", false);
    }
    //Jump animator settings
    IEnumerator jump( float delay)
    {
        yield return new WaitForSeconds( delay );
        animator.SetBool("Jumping", false);
        superMarioAnimator.SetBool("Jumping", false);
        fieryMarioAnimator.SetBool("Jumping", false); 
        animator.SetFloat("Speed",  0);
        superMarioAnimator.SetFloat("Speed", 0 );
        fieryMarioAnimator.SetFloat("Speed",  0);
        
    }
    IEnumerator TurnAround()
    {
        animator.SetBool("TurnAround", true);
        yield return new WaitForSeconds( 0.2f ) ;
        animator.SetBool("TurnAround", false);

    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void WaterSettings()
    {
        speed = 0.65f;
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        jumpForce = 1.4f;
    }
    public void NormalSettings()
    {
        speed = 1.0f;
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        jumpForce = 3.7f;
    }
}
