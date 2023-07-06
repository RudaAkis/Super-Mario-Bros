using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour
{
    public float [] JumpHeight; //Three different jump heights
    public float [] speed; //Three different speeds
    public Rigidbody2D rb;
    GameObject gc;
    GameController gameCtrl;
    void Start()
    {
        gameObject.transform.localScale = new Vector3(-1,1,1);
        rb.velocity = new Vector2(rb.velocity.x , JumpHeight[RandomNumber(0, 3)]);
    }
    void Update()
    {   
        rb.velocity = new Vector2(speed[RandomNumber(0, 3)] , rb.velocity.y);
    }
    public int RandomNumber(int low, int high)
    {
        return Random.Range(low, high);
    }
}
