using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypoint = 0;
    public float speed =  0.9f;
    int side = 1;
    
    void Update()
    {
        if(Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .1f)
        {
            currentWaypoint++;
            if (side == 1){ side = -1; speed = 0.9f;}
            else{ side = 1; speed = 1.3f;}
            gameObject.transform.localScale = new Vector3(1,side,1);
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
    }
}
