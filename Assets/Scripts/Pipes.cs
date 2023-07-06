using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform Destination;
    public Camera MainCam, UndergroundCam;
    GameObject Mario;
    Player_Movement pm;
    GameObject gc;
    GameController gameCtrl;
    public KeyCode enterKeyCodeDown = KeyCode.S;
    public KeyCode enterKeyCodeRight = KeyCode.D;
    Transform marioTransform;
    //Types of pipes that are selected though the entrance position
    public enum PipeType
    {
        right,
        left,
        down,
        none
    }
    public PipeType type;
    public int spawnDestination;
    GameObject spawner;// FinalLevelSpawner
    FinalLevelSpawner fls; // Final level Spawner Script
    bool spawned = false;

    public enum WaterSetting{
        entering,
        exiting,
        neither
    }
    [SerializeField] WaterSetting ws = WaterSetting.neither;

    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
        pm = Mario.GetComponent<Player_Movement>();
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();

        spawner = GameObject.FindGameObjectWithTag("MapSpawController");
        fls = spawner.GetComponent<FinalLevelSpawner>();
    }

    void OnTriggerStay2D(Collider2D other)
    {//If the pipe contains a destination and the player is staying on the trigger collider
        if (other.gameObject.CompareTag("Player") && Destination != null)
        {
            //Enter the pipe moving down if button s is pressed
            if(Input.GetKey(enterKeyCodeDown) && type == PipeType.down)
            {
                marioTransform = Mario.GetComponent<Transform>();
                //For the pipes in the final level making sure that it only spawns one level part when entering a certain pipe
                if (spawnDestination != 0 && !spawned)
                {
                    spawned = true;
                    fls.spawn(spawnDestination);
                    AssignDestination();
                }
                //Start the animation of mario entering the pipe
                StartCoroutine( EnterPipeDown( marioTransform, Destination) );
            }
            //Enter the pipe move right when the button d is pressed
            if(Input.GetKey(enterKeyCodeRight) && type == PipeType.right )
            {
                marioTransform = Mario.GetComponent<Transform>();
                //Start the animation of mario entering the pipe
                StartCoroutine(EnterPipeRight(marioTransform, Destination));
            }
        }
    }

    IEnumerator EnterPipeDown(  Transform marioTransform, Transform Destination)
    {
        checkWaterSetting();
        float elapsed = 0.0f;
        float duration = 0.7f;

        Vector3 from = marioTransform.position;
        Vector3 to = from + Vector3.down * 0.3f;

        while(elapsed < duration)
        { //How long each linear interpolation should be
            float t = elapsed / duration;
            //Lerp the object from , to a certain amount
            marioTransform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;// add the time that passed since the last frame

            yield return null;
        }
        //Make sure the object is actually at the destination position at the end for precision
        marioTransform.position = Destination.position;
        //Switch cameras
        MainCam.enabled = false;
        UndergroundCam.enabled = true;
        //Start the exiting of the pipe animation
        StartCoroutine(ExitPipeUp(marioTransform, Destination));
    }

    IEnumerator EnterPipeRight(  Transform marioTransform, Transform Destination)
    {
        checkWaterSetting();
        float elapsed = 0.0f;
        float duration = 0.7f;

        Vector3 from = marioTransform.position;
        Vector3 to = from + Vector3.right * 0.3f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            marioTransform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        marioTransform.position = Destination.position;
        MainCam.enabled = true;
        UndergroundCam.enabled = false;
        StartCoroutine(ExitPipeUp(marioTransform, Destination));
    }

    IEnumerator ExitPipeUp(  Transform marioTransform, Transform Destination)
    {
        checkWaterSetting();
        float elapsed = 0.0f;
        float duration = 0.7f;

        Vector3 from = marioTransform.position;
        Vector3 to = from + Vector3.up * 0.3f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            marioTransform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        //marioTransform.position = Destination.position; 
    }

    public void checkWaterSetting()
    {
        if (ws == WaterSetting.entering)
        {
            pm.WaterSettings();
        }
        else
        {
            pm.NormalSettings();
        }
    }

    void AssignDestination()
    {
        if(spawnDestination == 1 || spawnDestination == 3)
        {
            GameObject dest = GameObject.FindGameObjectWithTag("D");
            Destination = dest.transform;
        }
        if(spawnDestination == 5)
        {
            GameObject dest = GameObject.FindGameObjectWithTag("F");
            Destination = dest.transform;
        }
        if(spawnDestination == -1)
        {
            GameObject dest = GameObject.FindGameObjectWithTag("A");
            Destination = dest.transform;
            fls.spawn(-1);
        }
        if(spawnDestination == 7)
        {
            GameObject dest = GameObject.FindGameObjectWithTag("BL");
            Destination = dest.transform;
        }
        if (spawnDestination == 8)
        {
            GameObject dest = GameObject.FindGameObjectWithTag("G");
            Destination = dest.transform;
        }
    }
}
