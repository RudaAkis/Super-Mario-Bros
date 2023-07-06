using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class FlagPole : MonoBehaviour
{
    public GameObject flag;
    public GameObject flagsDestination;
    public Animator marioAnim, superMarioAnim, fieryMarioAnim;

    GameObject gc;
    GameController gameCtrl;

    public PlayableDirector director, superDirector, FieryDirector;

    public void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gc.GetComponent<GameController>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.tag == "Player" )
        {
            marioAnim.SetBool("LevelFinish", true);
            superMarioAnim.SetBool("LevelFinished", true);
            fieryMarioAnim.SetBool("LevelFinished", true);
            StartCoroutine(MoveFlag(flag.transform.position, flagsDestination.transform.position));
        }
        else
        { 
            marioAnim.SetBool("LevelFinish", false);
            superMarioAnim.SetBool("LevelFinished", false);
            fieryMarioAnim.SetBool("LevelFinished", false);
        }
    }

    IEnumerator MoveFlag(Vector3 from, Vector3 to)
    {
        float elapsed = 0.0f;
        float duration = 2f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            flag.transform.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        flag.transform.position = to;
        gameCtrl.incScore(1000);
        director.Play();
        superDirector.Play();
        FieryDirector.Play();
        Invoke("NextLevel", 3.0f);
    }

    void NextLevel()
    {
        gameCtrl.NextLevel();
        SceneManager.LoadScene($"{gameCtrl.level}-{gameCtrl.stage}");
    }
}
