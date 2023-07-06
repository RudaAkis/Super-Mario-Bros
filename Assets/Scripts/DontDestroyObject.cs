using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{

    public GameObject [] gameObjects;
    
    void Start()
    {   //Assign all the objects that need to be moved to the next scene to the array
        for(int i = 0; i < gameObjects.Length; i++)
        {
            DontDestroyOnLoad(gameObjects[i]);
        }
    }
}
