using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelSpawner : MonoBehaviour
{
    public GameObject LevelPartB;
    public GameObject LevelPartD;
    public GameObject LevelPartF;
    public GameObject BossPart;
    public GameObject waterPart;
    
    public float xAB = 4.001f;
    public float xAD = 6.67f;
    public float xBB = 11.56f;
    public float xBD = 12.558f;
    public float xDD = 11.369f;
    public float xDF = 9.917f;
    public float xFF = 13.282f;
    public float xBoss = 13.645f;
    public Transform PipeDestinationC;
    public Transform PipeDestinationE;
    public Transform PipeDestinationH;
    public float y;

    public float start_x;

    ArrayList levelPartsSpawned = new ArrayList();

    void Start()
    {
        // spawn(9);
        // spawn(1);
        // spawn(2);
        // spawn(3);
        // spawn(4);
        // spawn(5);
        // spawn(6);
        //Invoke("DeleteAllLevelParts", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn(int choice)
    {
        if (choice == -1)
        {
            Invoke("DeleteAllLevelParts", 2.0f);
        }
        if (choice == 9)    
        {
            start_x += xAB; 
            GameObject newLevelPart = Instantiate(LevelPartB, new Vector3(transform.position.x + start_x, transform.position.y -5.209616f, 0.0f), Quaternion.identity); 
            levelPartsSpawned.Add(newLevelPart);
        }
        //A TO D
        if (choice == 1)    
        {
            start_x += xAD; 
            GameObject newLevelPart = Instantiate(LevelPartD, new Vector3(transform.position.x + start_x, transform.position.y + 0.1603838f, 0.0f), Quaternion.identity); 
            levelPartsSpawned.Add(newLevelPart);
        }
        //B TO B
        if (choice == 2)    
        {
            start_x += xBB; 
            GameObject newLevelPart = Instantiate(LevelPartB, new Vector3(transform.position.x + start_x, transform.position.y -5.209616f, 0.0f), Quaternion.identity); 
            levelPartsSpawned.Add(newLevelPart);
        }
        //B TO D
        else if(choice == 3)
        {
            start_x += xBD;
            GameObject newLevelPart = Instantiate(LevelPartD, new Vector3(transform.position.x + start_x, transform.position.y + 0.1603838f + y, 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
        //D TO D
         else if(choice == 4)
        {
            start_x += xDD;
            GameObject newLevelPart = Instantiate(LevelPartD, new Vector3(transform.position.x + start_x, transform.position.y + 0.1603838f, 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
        //D TO F
        else if(choice == 5)
        {
            start_x += xDF;
            GameObject newLevelPart = Instantiate(LevelPartF, new Vector3(transform.position.x + start_x, transform.position.y + 1.840384f, 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
        //F TO F
        else if(choice == 6)
        {
            start_x += xFF;
            GameObject newLevelPart = Instantiate(LevelPartF, new Vector3(transform.position.x + start_x, transform.position.y + 1.840384f, 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
        else if (choice == 7)   
        {
            start_x += xBoss;
            GameObject newLevelPart = Instantiate(BossPart, new Vector3(transform.position.x + start_x, transform.position.y + 1.839616f , 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
        else if (choice == 8)
        {
            GameObject newLevelPart = Instantiate(waterPart, new Vector3(transform.position.x -10.0f , transform.position.y + 5.79f , 0.0f), Quaternion.identity);
            levelPartsSpawned.Add(newLevelPart);
        }
    }

    public void DeleteAllLevelParts()
    {
        foreach (GameObject item in levelPartsSpawned)
        {
            Destroy(item);
        }
    }
}
