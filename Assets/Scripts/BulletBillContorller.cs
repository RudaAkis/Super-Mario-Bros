using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBillContorller : MonoBehaviour
{
    public float speed = 0.5f; 
    public Rigidbody2D rb;
    GameObject gc;
    GameController gameCtrl;
    public bool IsMovingLeft;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
    }
    void Update()
    {
        if(IsMovingLeft)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }
    
    public void MoveLeft()
    {
        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
    }
    public void MoveRight()
    {
        rb.velocity = new Vector2(1 * speed, rb.velocity.y);
    }
    
}
