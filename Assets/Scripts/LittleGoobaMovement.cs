using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LittleGoobaMovement : MonoBehaviour
{   //Distance is 5.27
    GameObject gc;
    GameController gameCtrl;//game controller script
    GameObject player, superMario,fieryMario;//Mario object
    BoxCollider2D boxCollider;
    Rigidbody2D rigidbody;
    public Sprite deadGooba;
    float speed;
    public int direction = -1;
    public SpriteRenderer sr;

    GameObject Koopa;
    RedKoopaTroopa koopaControler;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        superMario = GameObject.FindWithTag("SuperMario");
        fieryMario = GameObject.FindGameObjectWithTag("FieryMario");

        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();

        rigidbody = GetComponent<Rigidbody2D>();
        direction = -1;
        speed = 0.3f;
    }
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) <= 3.0f)
        {
            rigidbody.velocity = new Vector2(direction * speed, rigidbody.velocity.y);
        }
    }
//(other.gameObject.CompareTag("KoopaTroopa") && KoopaTroopaShell.GetComponent<SpriteRenderer>().enabled == true)
    void OnCollisionEnter2D(Collision2D other)
    {
        //If collision with any of these obejcts switch direction
        if ( other.gameObject.tag == "LittleGooba" || other.gameObject.tag == "Beetle")
        {
            if (direction == -1){direction = 1;}
            else{direction = -1;}
        }
        //Destroy after falling
        if(other.gameObject.tag == "FallZone")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            //If player jumps on top implement death of the gooba 
            if ( other.transform.DotTest(transform, Vector2.down) )
            {
                Animator a = gameObject.GetComponent<Animator>();
                a.enabled = false;
                speed = 0;
                sr.sprite = deadGooba;
                boxCollider.size = new Vector2(0.13f, 0.043f);
                boxCollider.offset = new Vector2(0.0008f, -0.007f);
                gameObject.GetComponent<AudioSource>().enabled = true;
                gameCtrl.incScore(100);
            }
            //Implement form change if collision with gooba
            else if (other.gameObject.CompareTag("Player") && fieryMario.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameCtrl.SuperMario();
            }
            else if (other.gameObject.CompareTag("Player") && superMario.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameCtrl.SmallMario();
            }
            else
            {
                gameCtrl.loseOfLife();
            }
        }
        //Death from Koopa Troopa shell 
        if(other.gameObject.CompareTag("KoopaTroopa"))
        {

            if (direction == -1){direction = 1;} else{direction = -1;}
        }
        //Death by fireball
        if(other.gameObject.CompareTag("Fireball"))
        {
            transform.rotation =  Quaternion.Euler(0, 0, 180);
            StartCoroutine(destroyEnemeyAfterDelay(0.3f, transform));
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
    //Destroy object after a certain delay
    IEnumerator destroyEnemeyAfterDelay( float delay, Transform goomba)
    {
        goomba.position = new Vector3(goomba.position.x, goomba.position.y + 0.151f, goomba.position.z);
        yield return new WaitForSeconds( delay ) ;
        Destroy(goomba.gameObject);
    }
}
