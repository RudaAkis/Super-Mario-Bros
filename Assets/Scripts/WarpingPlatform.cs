using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpingPlatform : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    [SerializeField] float speed = 0.2f;
    
    void Update()
    {
        if(Vector3.Distance(transform.position, waypoints[0].transform.position) < .1f)
        {
            transform.position = waypoints[1].transform.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[0].transform.position, speed * Time.deltaTime);
    }
}
