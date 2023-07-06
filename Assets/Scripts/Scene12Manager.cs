using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene12Manager : MonoBehaviour
{

    public GameObject Mario; 
    public Transform StartingPosition;  

    // Start is called before the first frame update
    void Start()
    {
        Mario.transform.position = StartingPosition.position;
    }
}
