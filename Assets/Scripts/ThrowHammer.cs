using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHammer : MonoBehaviour
{//Basically the same script as for the Jumping Fish

    public float ThrowHeight; //Three different jump heights
    public float speed; //Three different speeds
    public Rigidbody2D rb;
    GameObject gc;
    GameController gameCtrl;
    void Start()
    {
        rb.velocity = new Vector2(rb.velocity.x , ThrowHeight);
        StartCoroutine(destroyEnemeyAfterDelay(3.0f, gameObject));
    }
    void Update()
    {   
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    IEnumerator destroyEnemeyAfterDelay( float delay, GameObject hammer)
    {
        yield return new WaitForSeconds( delay ) ;
        Destroy(hammer);
    }
}
