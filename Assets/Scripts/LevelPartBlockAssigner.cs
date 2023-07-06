using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartBlockAssigner : MonoBehaviour
{
    public int blockNumber;
    GameObject MapController;
    MapSpawController msc;
    // Start is called before the first frame update
    void Start()
    {
        MapController = GameObject.FindGameObjectWithTag("MapSpawController");
        msc = MapController.GetComponent<MapSpawController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            msc.spawn(blockNumber);
        }
    }
}
