using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    GameObject gc;
    GameController gameCtrl;
    SpriteRenderer sr;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameCtrl.incScore(100);
            gameCtrl.incCoins();
            Destroy(gameObject);
        }
    }
}
