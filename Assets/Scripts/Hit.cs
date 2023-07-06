
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Playables;

public class Hit : MonoBehaviour
{
    public Sprite afterHitBLock;
    public int hits;
    private bool isAnimating;
    public GameObject itemInBlock;
    GameObject Mario;
    GameObject SuperMario;
    GameObject fieryMario;

    GameObject gc;
    GameController gameCtrl;

    public Animator anim;

    bool startSpawn;
    float spawnX;
    float spawnY;
    float spawZ;

    public void Start()
    {
        startSpawn = false;
        Mario = GameObject.FindGameObjectWithTag("Player");
        SuperMario = GameObject.FindGameObjectWithTag("SuperMario");
        fieryMario = GameObject.FindGameObjectWithTag("FieryMario");

        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
    }
    void Update()
    {
        if (startSpawn)
        {
            StartCoroutine(GrowBeanStem());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {   //Will only activate if number of hits is not equal to zero and if collision is with player
        if ( !isAnimating && hits != 0 && other.gameObject.CompareTag("Player"))
        {   //If the player is moving up during a collision
            if(other.transform.DotTest(transform, Vector2.up))
            {
                //If in super or fiery forms destroy the breakable brick
                if(gameObject.CompareTag("BreakableBrick") && SuperMario.GetComponent<SpriteRenderer>().enabled == true || gameObject.CompareTag("BreakableBrick") && fieryMario.GetComponent<SpriteRenderer>().enabled == true)
                    {
                        StartCoroutine(destroyAfterDelay(0.4f, gameObject, anim));
                    }
                //If a coin block is hit adjust the coin score and points
                if(gameObject.CompareTag("CoinBlock") )
                {
                    gameCtrl.incCoins();
                    gameCtrl.incScore(200);
                }
                //perform the action of hitting the brick(move it up and down) plus depending on what the brick type is perform the actions specific to the brick
                hit(gameObject);
            }
        }
    }

    private void hit(GameObject brick)
    {
        hits--;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (hits == 0)
        {
            sr.sprite = afterHitBLock;
        }
        StartCoroutine( AnimateJumpingBlocks(brick) );
        //If it is a coin block spawn a coin object
        if(gameObject.CompareTag("CoinBlock")){
            anim.SetBool("Hit", true);
            Instantiate(itemInBlock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, 0.0f), Quaternion.identity);
        }//If it is an invisible block make it visible and spawn a coin
        if (gameObject.CompareTag("InvisibleBlock"))
        {
            anim.SetBool("Hit", true);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Instantiate(itemInBlock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, 0.0f), Quaternion.identity);
        }//If it is a block from which the bean stem grows start spawning a bean stem
        if (gameObject.CompareTag("BeanBlock"))
        {
            startSpawn = true;
        }
    }

    private IEnumerator AnimateJumpingBlocks(GameObject brick)
    {
        isAnimating = true;

        //Get the current and the end position of the block
        Vector3 currentPosition = transform.localPosition;// Current position
        Vector3 goalPosition = currentPosition + Vector3.up * 0.1f; // Current position plus 0.1 facing up

        yield return MoveBlock(currentPosition, goalPosition);//Move up
        yield return MoveBlock(goalPosition, currentPosition);//Move down

        isAnimating = false;
    }

    private IEnumerator MoveBlock(Vector3 from, Vector3 to)
    {
        float elapsed = 0.0f;
        float duration = 0.125f;

        while(elapsed < duration)
        {
            //How long each linear interpolation should be
            float time = elapsed / duration;
            //Lerp the object from , to a certain amount
            transform.localPosition = Vector3.Lerp(from, to, time);
            elapsed += Time.deltaTime;// add the time that passed since the last frame

            yield return null;
        }
        //Make sure the object is actually at the destination position at the end for precision when going back
        transform.localPosition = to;
    }

    public IEnumerator GrowBeanStem()
    {
        startSpawn = false;
        Instantiate(itemInBlock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        startSpawn = true;
    }
    
    IEnumerator destroyAfterDelay( float delay, GameObject brick, Animator anim)
    {
        anim.SetBool("Destroy", true);
        yield return new WaitForSeconds( delay ) ;
        anim.SetBool("Destroy", false);
        Destroy(brick);
    }
}
