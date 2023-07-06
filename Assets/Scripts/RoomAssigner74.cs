using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAssigner74 : MonoBehaviour
{
    GameObject MapController;
    MapSpawController msc;
    public enum Choices{
        correct,
        incorrect
    }
    public enum TriggerType{
        final,
        assigner
    }
    public Choices Choice;
    public TriggerType Type;
    // Start is called before the first frame update
    void Start()
    {
        MapController = GameObject.FindGameObjectWithTag("MapSpawController");
        msc = MapController.GetComponent<MapSpawController>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //If the trigger is the on final trigger that can be triggered spawn the new block part
            if (Type == TriggerType.final )
            {
                if (Choice == Choices.correct){ msc.multiplicationResult *= 1; }
                else{ msc.multiplicationResult *= 0; }
                //Since the multiplication result is multiplied each time a trigger is touched by either 1 or 0 if it is still 1 at the end
                // Spawn the new level part else spawn the same level part again and reset the multiplication result
                if (msc.multiplicationResult == 1)
                {
                    msc.currentPartNumber++;
                    msc.spawn(msc.currentPartNumber);
                }
                else
                {
                    msc.spawn(msc.currentPartNumber);
                    msc.multiplicationResult = 1;
                }
            }
            else 
            {
                if (Choice == Choices.correct){ msc.multiplicationResult *= 1; }
                else{ msc.multiplicationResult *= 0; }
            }
        }
    }
}
