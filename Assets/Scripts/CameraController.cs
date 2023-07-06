using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    Player_Movement pm;

    void Start()
    {
        offset = new Vector3(1, 0, 0);
        pm = player.GetComponent<Player_Movement>();
    }

    void Update()
    {
        if (pm.horizontalInput > 0 && player.transform.position.x >= pm.last_x_position )
        {
            pm.last_x_position = player.transform.position.x;
            // UnityEngine.Debug.Log( "Last position x" + pm.last_x_position );
            // UnityEngine.Debug.Log( "Current x" + player.transform.position.x );
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z) + offset;
        }
    }
}
