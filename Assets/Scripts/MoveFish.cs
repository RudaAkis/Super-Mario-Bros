using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    public float speed = 0.5f; 
    public Rigidbody2D rb;
    GameObject gc;
    GameController gameCtrl;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = Vector2.left * speed;
    }
    
    
}
