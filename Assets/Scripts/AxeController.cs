using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AxeController : MonoBehaviour
{
    GameObject gc;
    GameController gameCtrl;
    public GameObject [] brideToDestroy;
    int counter;
    bool canProceed;
    float delay;
    public GameObject Bridge;
    void Start()
    {
        canProceed = false;
        counter = 0;
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
        delay = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Bridge.GetComponent<BoxCollider2D>().enabled = false;
            foreach(GameObject bridge in brideToDestroy)
            {
                delay +=0.3f;
                StartCoroutine(destroyAfterDelay(delay, bridge));
            }
            StartCoroutine(Next(2.5f));
        }
    }
    IEnumerator destroyAfterDelay( float delay, GameObject bridge)
    {
        yield return new WaitForSeconds( delay ) ;
        Destroy(bridge);
    }
    IEnumerator Next( float delay)
    {
        yield return new WaitForSeconds( delay ) ;
        gameCtrl.NextLevel();
        SceneManager.LoadScene($"{gameCtrl.level}-{gameCtrl.stage}");
    }
}
