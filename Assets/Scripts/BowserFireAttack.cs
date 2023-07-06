using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserFireAttack : MonoBehaviour
{
    public float speed = 0.5f; 
    public Rigidbody2D rb;
    public float delay = 3.0f;
    GameObject gc;
    GameController gameCtrl;

    void Start() {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine( destroyAfterDelay(delay) );
    }

    void Update() {
        // Apply a force to the Rigidbody2D component
        rb.velocity = Vector2.left * speed;
    }

    public IEnumerator destroyAfterDelay(float delay)
    {   
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
