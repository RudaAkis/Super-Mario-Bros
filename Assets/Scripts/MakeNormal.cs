using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNormal : MonoBehaviour
{
    GameObject player;
    Player_Movement pm;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<Player_Movement>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            pm.SetSpeed(1.0f);
        
        }
    }
}
