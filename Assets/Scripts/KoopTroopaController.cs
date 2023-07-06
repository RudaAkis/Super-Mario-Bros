using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopTroopaController : MonoBehaviour
{    //Distance is 5.27
    GameObject gc;
    GameController gameCtrl;
    GameObject player;
    GameObject superMario;
    Rigidbody2D rigidbody;
    float speed;
    public int direction;
    public GameObject KoopaShell;
    public Sprite deadTroopa;
    public Sprite shellMoving;
    public SpriteRenderer sr;
    SpriteRenderer DeadKoopaSR;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        DeadKoopaSR = KoopaShell.GetComponent<SpriteRenderer>();
        DeadKoopaSR.enabled = false;

        player = GameObject.FindWithTag("Player");
        superMario = GameObject.FindWithTag("SuperMario");

        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();

        rigidbody = GetComponent<Rigidbody2D>();
        direction = -1;
        speed = 0.3f;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) <= 5.3)
        {
            rigidbody.velocity = new Vector2(direction * speed, rigidbody.velocity.y);
        }
        if(direction < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "SolidBlock" || other.gameObject.tag == "LittleGooba" && DeadKoopaSR.enabled == false)
        {
           if (direction == -1){direction = 1;}
            else{direction = -1;} 
        }
        if(other.gameObject.tag == "FallZone")
        {
            Destroy(gameObject);
        }
        //All collisions related to the player
        if(other.gameObject.CompareTag("Player")) 
        {
            //If player is jumping on him
            if ( other.transform.DotTest(transform, Vector2.down) && sr.enabled == true )
            {
                gameObject.GetComponent<Animator>().enabled = false;
                speed = 0;
                boxCollider.size = new Vector2(0.1286669f, 0.1351122f);
                boxCollider.offset = new Vector2(0.007565975f, 0.01749301f);
                sr.enabled = false;
                DeadKoopaSR.enabled = true;
            }
            else if ( (DeadKoopaSR.enabled == true && other.transform.DotTest(transform, Vector2.down) && other.transform.position.x < transform.position.x) ||
            (DeadKoopaSR.enabled == true && other.transform.position.x < transform.position.x ) )
            {
                speed = 1.5f;
                direction = 1;
            }
            else if ( (DeadKoopaSR.enabled == true && other.transform.DotTest(transform, Vector2.down) && other.transform.position.x > transform.position.x) ||
            (DeadKoopaSR.enabled == true && other.transform.position.x > transform.position.x ) )
            {
                speed = 1.5f;
                direction = -1;
            }
            else
            {
                gameCtrl.loseOfLife();
            }
        }
        if(other.gameObject.CompareTag("Fireball"))
        {
            transform.rotation =  Quaternion.Euler(0, 0, 180);
            StartCoroutine(destroyEnemeyAfterDelay(0.3f, transform));
        }
    }
    IEnumerator destroyEnemeyAfterDelay( float delay, Transform koopa)
    {
        koopa.position = new Vector3(koopa.position.x, koopa.position.y + 0.151f, koopa.position.z);
        yield return new WaitForSeconds( delay ) ;
        Destroy(koopa.gameObject);
    }
}
    