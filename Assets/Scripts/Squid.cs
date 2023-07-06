using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
     public float speed = 0.2f; 
    public Rigidbody2D rb;
    GameObject gc;
    GameController gameCtrl;
    GameObject player;
    enum WalkDirections{
        up,
        down
    }
    WalkDirections NextDirection;
    void Start()
    {
        NextDirection = WalkDirections.up;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5.3)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //if(NextDirection == WalkDirections.up) { StartCoroutine(MoveUp()); }
            //else {  StartCoroutine(MoveDown()); }
        }
    }
    public IEnumerator MoveUp()
    {
        StopCoroutine(MoveDown());
        transform.position = Vector2.MoveTowards(transform.position, Vector2.up * 0.1f, Time.deltaTime);
        yield return new WaitForSeconds(1.1f);
        NextDirection = WalkDirections.down;      
        yield return null;                                                          
    }
    //Walk towards right for 0.5 of a second
    public IEnumerator MoveDown()
    {
        StopCoroutine(MoveUp());
        transform.position = Vector2.MoveTowards(transform.position, Vector2.down * 0.1f, Time.deltaTime);
        yield return new WaitForSeconds(1.1f);
        NextDirection = WalkDirections.up;
        yield return null; 
    }    
}
