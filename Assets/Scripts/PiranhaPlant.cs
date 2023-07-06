using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    bool isMoving;
    bool isDelay;
    GameObject player;
    [SerializeField] Transform [] waypoints;
    Transform dest1;
    Transform dest2;
    public Vector3 currentDest;
    int currentWaypoint = 0;
    [SerializeField] float speed = 0.1f;
    bool canSwitch = true;
    bool moveUp;
    bool moveDown;
    bool canMove = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dest1 = waypoints[0];
        dest2 = waypoints[1];
        currentDest = dest2.position;
        moveUp = true;
        moveDown = false;
    }
    void Update()
    {
        if (canMove)
        {
            move();
        }
    }
    public void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDest, speed * Time.deltaTime);
        StartCoroutine(MoveTimer());
    }
    public IEnumerator MoveTimer ()
    {
        if (canSwitch)
        {
            canSwitch = false;
            SwitchDirection();
            yield return new WaitForSeconds(5f);
            canSwitch = true;
        }
    }
    public void SwitchDirection()
    {
        StartCoroutine(delay());
        //Assign the next destination
        if (moveUp){ currentDest = dest2.position; moveDown = true; moveUp = false;}
        else{ currentDest = dest1.position; moveDown = false; moveUp = true; }
    }//Delay when the waypoint is reached
    public IEnumerator delay()
    {   
        canMove = false;
        yield return new WaitForSeconds(2.7f);
        canMove = true;
    }










    //Swtich the goal and starting position
    // public void SwitchPositions ()
    // {
    //     if (counter == 0){ currentPosition = gameObject.transform.position; goalPosition = tempstart; counter = 1; }
    //     else{ currentPosition =  gameObject.transform.position; goalPosition = tempGoal; counter = 0;}
    // }

    // private IEnumerator AnimatePiranhaPlant(Vector3 currentPosition, Vector3 goalPosition)
    // {
    //     isMoving = true;
    //     elapsed = 0.1f;
    //     yield return Move(currentPosition, goalPosition,elapsed);
    //     yield return Move(goalPosition, currentPosition,elapsed);
    //     isMoving = false;
    // }

    // private IEnumerator Move(Vector3 from, Vector3 to,float elapsed)
    // {
    //     float duration = 1.5f;

    //     while(elapsed < duration)
    //     {
    //         float t = elapsed / duration;

    //         transform.localPosition = Vector3.Lerp(from, to, t);
    //         elapsed += Time.deltaTime;

    //         yield return null;
    //     }
    //     transform.localPosition = to;
    // }
}
