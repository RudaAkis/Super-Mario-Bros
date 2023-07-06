using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject GreyFish;
    public enum FishTypes
    {
        jumping,
        swimming,
        bullet
    }
    public FishTypes FishType;
    [SerializeField] Transform [] SpawnPoints;
    float [] x_SpawnValues = {-0.25f, -0.75f, -1.25f, 0.25f, 0.75f, 1.25f};
    GameObject player;
    public float cooldown = 4.0f;
    bool IsOnCooldown;
    BulletBillContorller bbc;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        IsOnCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOnCooldown) { SpawnFish(); }
    }

    public void SpawnFish()
    {   
        if (FishType == FishTypes.swimming)
        {//Spawns the fish at one of 10 random position on the right side outside of the screen
            int random = RandomNumber(0,10);
            Instantiate(GreyFish, new Vector3(SpawnPoints[random].position.x , SpawnPoints[random].position.y, 0.0f), Quaternion.identity);
            StartCoroutine(SpawnCooldown(cooldown));
        }
        else if (FishType == FishTypes.bullet)
        {   
            bbc = GreyFish.GetComponent<BulletBillContorller>();
            int random = RandomNumber(0,10);
            bbc.IsMovingLeft = true;
            Instantiate(GreyFish, new Vector3(SpawnPoints[random].position.x, SpawnPoints[random].position.y, 0.0f), Quaternion.identity);
            StartCoroutine(SpawnCooldown(cooldown));
        }
        else
        {// Screen widdth 4.00 Tai negalima atimt ir pridet daugiau dvieju -+0.25 -+0.75 -+1.25
            int random = RandomNumber(0,7);
            //Spawns the fish at the bottom at 1 of six random values either added or subtracted from marios position
            Instantiate(GreyFish, new Vector3(player.transform.position.x - x_SpawnValues[random], 0.0f, 0.0f), Quaternion.identity);
            StartCoroutine(SpawnCooldown(cooldown));
        }
    }
    public IEnumerator SpawnCooldown(float cooldown)
    {
        IsOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        IsOnCooldown = false;
    }
    public int RandomNumber(int low, int high)
    {
        return Random.Range(low, high);
    }
}
