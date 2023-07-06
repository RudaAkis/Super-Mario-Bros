using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    They need to be able to move up and down the platform ignoring the bottom part or the top part of the collider
*/
public class HammerBrotherController : MonoBehaviour
{
    public GameObject Hammer;
    Rigidbody2D rb;
    public float JumpHeight;
    bool isShortCooldown, isLongCooldown;
    bool MoveVerticalCooldown; 
    public float LongDelay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveVerticalCooldown = false;
    }
    void Update()
    {
        if(!isLongCooldown)
        {
            StartCoroutine(SpawnHammers());
        }      
        if(!MoveVerticalCooldown)
        {
            if ( RandomNumber(0,2) == 0 ){ StartCoroutine(JumpUp()); }
            else{ StartCoroutine(JumpDown()); }
        }
    }
    public IEnumerator JumpUp()
    {
        MoveVerticalCooldown = true;
        rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        StartCoroutine(SetIgnoreCollisions());
        yield return new WaitForSeconds(4.0f);
        MoveVerticalCooldown = false;
    }
    public IEnumerator JumpDown()
    {
        MoveVerticalCooldown = true;
        StartCoroutine(SetIgnoreCollisions());
        yield return new WaitForSeconds(4.0f);
        MoveVerticalCooldown = false;
    }
    public void Move()
    {

    }
    public void SwitchDirection()
    {
        
    }
    public IEnumerator SetIgnoreCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = LayerMask.NameToLayer("HammerBrother");
    }
    public IEnumerator SpawnHammers()
    {  
        StartCoroutine(LongCooldown(LongDelay));
        //Spawn 4 hammers with a 0.2 second cooldown between each of them
        Instantiate(Hammer, new Vector3(transform.position.x , transform.position.y, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(Hammer, new Vector3(transform.position.x , transform.position.y, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(Hammer, new Vector3(transform.position.x , transform.position.y, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(Hammer, new Vector3(transform.position.x , transform.position.y, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        yield return null; 
    }
    //Long cooldown is used for the time inbetween thowing all 4 hammers
    public IEnumerator LongCooldown(float Cooldown)
    {
        isLongCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        isLongCooldown = false;
    }
    //Short cooldown is used for the time in between each hammer
    public IEnumerator ShortCooldown(float Cooldown)
    {
        isShortCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        isShortCooldown = false;
    }
    public int RandomNumber(int low, int high)
    {
        return Random.Range(low, high);
    }
}
