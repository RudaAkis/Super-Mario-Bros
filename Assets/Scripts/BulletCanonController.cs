using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCanonController : MonoBehaviour
{
    public Transform SpawnTop;
    public GameObject BulletBill;
    BulletBillContorller bbc;
    BulletBillContorller fbbc;
    bool canSpawn = true;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bbc = BulletBill.GetComponent<BulletBillContorller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            {
            if(player.transform.position.x < gameObject.transform.position.x)
            {
                if (Mathf.Abs( player.transform.position.x - gameObject.transform.position.x)<= 4.5f)
                {
                    SpawnLeft();  
                }
                
            }
            else
            {
                if (Mathf.Abs( player.transform.position.x - gameObject.transform.position.x)<= 4.5f)
                {
                    SpawRight();
                }
            }
        }
    }

    public void SpawnLeft()
    {
        BulletBill.transform.localScale = new Vector3(1,1,1);
        bbc.IsMovingLeft = true;
        Instantiate(BulletBill, new Vector3(SpawnTop.position.x, SpawnTop.position.y, 0.0f), Quaternion.identity);
        StartCoroutine(CooldownDelay());
    }
    public void SpawRight()
    {
        BulletBill.transform.localScale = new Vector3(-1,1,1);
        bbc.IsMovingLeft = false;
        Instantiate(BulletBill, new Vector3(SpawnTop.position.x, SpawnTop.position.y, 0.0f), Quaternion.identity);
        StartCoroutine(CooldownDelay());
    }

    public IEnumerator CooldownDelay()
    {
        canSpawn = false;
        yield return new WaitForSeconds(5.0f);
        canSpawn = true;
    }
}
