using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawController : MonoBehaviour
{
    public float xA;
    public float xB;
    public float xBoss;
    public float y;
    public GameObject LevelPartA;
    public GameObject LevelPartB;
    public GameObject BossPart;
    public float start_x;
    bool spawnA = true;
    bool spawnB = false;
    bool canSpawn = true;
    public int multiplicationResult;
    public int currentPartNumber;
    
    void Start()
    {
        currentPartNumber = 1;
    }
    public void spawn(int choice)
    {
        if (choice == 1)    
        {
            start_x += xA; 
            Instantiate(LevelPartA, new Vector3(transform.position.x + start_x, transform.position.y, 0.0f), Quaternion.identity); 
            spawnA = false;
            spawnB = true;
        }
        else if(choice == 2)
        {
            start_x += xB;
            Instantiate(LevelPartB, new Vector3(transform.position.x + start_x, transform.position.y + y, 0.0f), Quaternion.identity);
            xB = 10.04f;
            spawnB = false;
        }
        else
        {
            start_x += xBoss;
            Instantiate(BossPart, new Vector3(transform.position.x + start_x, transform.position.y + y, 0.0f), Quaternion.identity);
            canSpawn = false;
        }
    }
}
