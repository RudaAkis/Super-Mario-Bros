using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalLevelPartAssigner : MonoBehaviour
{
    GameObject spawner;// FinalLevelSpawner
    FinalLevelSpawner fls; // Final level Spawner Script

    public int partNumber;
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("MapSpawController");
        fls = spawner.GetComponent<FinalLevelSpawner>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            fls.spawn(partNumber);
        }
    }
}
