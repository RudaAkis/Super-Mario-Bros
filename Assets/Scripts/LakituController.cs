using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituController : MonoBehaviour
{
    float [] x_moveValues = {-0.25f, -0.5f, 0.25f, 0.75f, 1.5f};
    GameObject Mario;
    float speed = 0.01f;
    public float maxSpeed = 0.4f;
    public float acceleration = 0.0000001f;
    public SpriteRenderer sr;
    public Sprite InCloudSprite;
    public GameObject Spiky;
    bool isMoving = false;
    bool canMove = true;
    bool spikyOnCooldown = false;
    Vector3 goalPosition;
    void Start()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(SpikyCooldown());
    }
    void Update()
    {
        Debug.Log(goalPosition.x);
        if(canMove)
        {if(!isMoving)
            {
                int rndNumber = RandomNumber(0,3);
                //Randomise whether to change position to move to or fly right above mario
                if (rndNumber == 0)
                {
                    goalPosition = new Vector3( Mario.transform.position.x + x_moveValues[RandomNumber(0,5)] ,gameObject.transform.position.y, gameObject.transform.position.z );
                }
                else
                {
                    goalPosition = new Vector3( Mario.transform.position.x ,gameObject.transform.position.y, gameObject.transform.position.z);
                }
            }
            Move(goalPosition);
        }//If it is not not cooldown drop spinny
        if (!spikyOnCooldown) { DropSpiky(); }
    }
    public void Move(Vector3 goalPosition)
    {
        isMoving = true;//While moving cant drop spiky
        if (speed < maxSpeed){ speed += acceleration; }
        //Move it to one the assigned position which is retrieved from Marios position
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
        //Stop, make the boolean value false initiate a delay for either getting a new position or dropping spinny
        if(transform.position == goalPosition){StartCoroutine(Stop());}
    }
    public void DropSpiky()
    {
        Instantiate(Spiky, new Vector3(transform.position.x , transform.position.y, 0.0f), Quaternion.identity);
        StartCoroutine(SpikyCooldown());
    }
    public IEnumerator Stop()
    {
        isMoving = false;
        canMove = false;
        speed = 0.1f;
        yield return new WaitForSeconds(0.7f);
        canMove = true;
    }
    public IEnumerator SpikyCooldown()
    {
        spikyOnCooldown = true;
        yield return new WaitForSeconds(3.5f);
        spikyOnCooldown = false;
    }
    public int RandomNumber(int low, int high)
    {
        return Random.Range(low, high);
    }
}
