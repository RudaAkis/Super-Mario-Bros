using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject Mario;
    Player_Movement pm;
    Rigidbody2D rigidbody;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        pm = Mario.GetComponent<Player_Movement>();
        
        StartCoroutine( destroyAfterDelay(3.0f) );
        if (pm.direction < 0) { dir = -1;}
        else{ dir = 1; }
    }

    // Update is called once per frame
    void Update()
    {
       rigidbody.velocity = new Vector2(dir * 1.2f, rigidbody.velocity.y); 
    }

    IEnumerator destroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds( delay ) ;
        Destroy(gameObject);
    }
}
