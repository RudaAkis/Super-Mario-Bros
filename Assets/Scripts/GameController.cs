using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject Mario, superMario, fieryMario;
    Player_Movement pm;
    BoxCollider2D boxCollider;

    public SpriteRenderer smallMarioRenderer, superMarioRenderer, fieryMarioRenderer;
    Animator superMarioAnimator, smallMarioAnimator, fieryMarioAnimator;
    [HideInInspector]
    public int LivesNumber, CoinNumber, level, stage, time, Score;
    [HideInInspector]
    public float Timer;

    public Sprite deadMario;

    public Text LivesText, CoinsText, WorldText, TimeText, ScoreText;

    bool FormAnimatingAllowed;
    public GameObject scoreTxtPrefab;
    public GameObject scoreTxt;
    
    public enum MarioForms{
        Small,
        Super,
        Fiery,
        Dead
    }
    public bool damaged = false;

    MarioForms CurrentForm;
    // Start is called before the first frame update
    void Start()
    {
        pm = Mario.GetComponent<Player_Movement>();
        CurrentForm = MarioForms.Small;
        level = 1;
        stage = 1;
        LivesNumber = 3;
        CoinNumber = 0;
        Timer = 399f;
        Score = 0;
        time = 0;
        
        Mario = GameObject.FindGameObjectWithTag("Player");
        superMario = GameObject.FindGameObjectWithTag("SuperMario");
        fieryMario = GameObject.FindGameObjectWithTag("FieryMario");

        smallMarioRenderer = Mario.GetComponent<SpriteRenderer>();
        superMarioRenderer = superMario.GetComponent<SpriteRenderer>();
        fieryMarioRenderer = fieryMario.GetComponent<SpriteRenderer>();

        superMarioAnimator = superMario.GetComponent<Animator>();
        smallMarioAnimator = Mario.GetComponent<Animator>();
        fieryMarioAnimator = fieryMario.GetComponent<Animator>();
        
        boxCollider = Mario.GetComponent<BoxCollider2D>();
        
        smallMarioAnimator.SetBool("Dead", false);
    }   

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        LivesText.text = LivesNumber.ToString(); 
        CoinsText.text = CoinNumber.ToString();
        WorldText.text = level.ToString() + " - " + stage.ToString();
        ScoreText.text = Score.ToString();
        time = (int)Timer;
        TimeText.text = time.ToString();
    }

    public void SmallMario()
    {
        if (damaged == false)
        {
            damaged = true;
            Invoke("makeDamagedFalse", 2.0f);
            CurrentForm = MarioForms.Small;
            if (FormAnimatingAllowed)
            {
                StartCoroutine(AnimateSmallTransition(0.5f, smallMarioAnimator));
            }
            FormAnimatingAllowed = false;   
            smallMarioRenderer.enabled = true;
            smallMarioAnimator.enabled = true;
            
            superMarioRenderer.enabled = false;
            superMarioAnimator.enabled = false;
        
            fieryMarioRenderer.enabled = false;
            fieryMarioAnimator.enabled = false;

            //Change the collider size and position to fit the SuperMario sprite
            boxCollider.size = new Vector2(0.07316589f, 0.1191856f);
            boxCollider.offset = new Vector2(-0.001773357f, -0.02035207f);
        }
    }
    public void SuperMario()
    {
        if (damaged == false)
        {
            damaged = true;
            Invoke("makeDamagedFalse", 2.0f);
            CurrentForm = MarioForms.Super;
            if (FormAnimatingAllowed)
            {
                StartCoroutine(AnimateSuperTransition(0.5f, superMarioAnimator)); 
            }
            FormAnimatingAllowed = false;
            smallMarioRenderer.enabled = false;
            smallMarioAnimator.enabled = false;

            superMarioRenderer.enabled = true;
            superMarioAnimator.enabled = true;

            fieryMarioRenderer.enabled = false;
            fieryMarioAnimator.enabled = false;

            //Change the collider size and position to fit the SuperMario sprite
            boxCollider.size = new Vector2(0.09919f, 0.2814952f);
            boxCollider.offset = new Vector2(-0.00177f, 0.06080276f);
        }
        
    }
    public void FieryMario()
    {
        if (damaged == false)
        {
            damaged = true;
            Invoke("makeDamagedFalse", 2.0f);
            CurrentForm = MarioForms.Fiery;
            if(FormAnimatingAllowed)
            {
                StartCoroutine(AnimateFieryTransition(0.5f, fieryMarioAnimator));
            }
            FormAnimatingAllowed = false;
            smallMarioRenderer.enabled = false;
            smallMarioAnimator.enabled = false;
            
            superMarioRenderer.enabled = false;
            superMarioAnimator.enabled = false;
        
            fieryMarioRenderer.enabled = true;
            fieryMarioAnimator.enabled = true;

            
            boxCollider.size = new Vector2(0.09919f, 0.2814952f);
            boxCollider.offset = new Vector2(-0.00177f, 0.06080276f);
        }
    }


    IEnumerator AnimateSuperTransition( float delay, Animator superMarioAnimator)
    {
        superMarioAnimator.SetBool("Transition", true);
        yield return new WaitForSeconds( delay ) ;
        superMarioAnimator.SetBool("Transition", false);
    }

    IEnumerator AnimateSmallTransition( float delay, Animator smallMarioAnimator)
    {
        smallMarioAnimator.SetBool("Transition", true);
        yield return new WaitForSeconds( delay ) ;
        smallMarioAnimator.SetBool("Transition", false);
    }

    IEnumerator AnimateFieryTransition( float delay, Animator fieryMarioAnimator)
    {
        fieryMarioAnimator.SetBool("Transition", true);
        yield return new WaitForSeconds( delay ) ;
        fieryMarioAnimator.SetBool("Transition", false);
    }

    public void Death()
    {
            smallMarioRenderer.sprite = deadMario;
            smallMarioAnimator.SetBool("Dead", true);
            UnityEngine.Debug.Log("Dead");
            pm.PlayerJump();
            pm.PlayerJump();
            boxCollider.enabled = false;
            CurrentForm = MarioForms.Dead;
    }
    public void incCoins()
    {
        CoinNumber++;
    }
    public void incScore(int score)
    {
        Score = Score + score;//Increment score
        //Instantiate new text object
        scoreTxt = Instantiate(scoreTxtPrefab, new Vector3(Mario.transform.position.x , Mario.transform.position.y, 0.0f), Quaternion.identity);
        scoreTxt.GetComponent<TextMeshPro>().text = score.ToString();//Assign the score value of the specific object
        StartCoroutine(destroyPointsAfterDelay(1.0f, scoreTxt));//lerp  text upwards for 1 second then destroy it
    }
    
    public void loseOfLife()
    {
        if (damaged == false)
        {
            damaged = true;
            Invoke("makeDamagedFalse", 2.0f);
            if (CurrentForm == MarioForms.Fiery) { SuperMario(); }
            else if (CurrentForm == MarioForms.Super) { SmallMario(); }
            else 
            {
                LivesNumber -= 1;
                Death();
                Invoke("restartLevel", 4.0f);
            }
            if (LivesNumber == 0){ level = 1; stage = 1;SceneManager.LoadScene($"{level}-{stage}"); }     
        }
    }
    public void makeDamagedFalse()
    {
        damaged = false;
    }
    public void restartLevel()
    {
        SceneManager.LoadScene($"{level}-{stage}");
    }
    //Advance to the next level     
    public void NextLevel()
    {
        if (stage == 4) { level++;stage = 1; }
        else { stage++; }
    }
    public void StageStart()
    {
        FormAnimatingAllowed = false;
        Timer = 399;
        Mario = GameObject.FindGameObjectWithTag("Player");
        superMario = GameObject.FindGameObjectWithTag("SuperMario");
        fieryMario = GameObject.FindGameObjectWithTag("FieryMario");

        smallMarioRenderer = Mario.GetComponent<SpriteRenderer>();
        superMarioRenderer = superMario.GetComponent<SpriteRenderer>();
        fieryMarioRenderer = fieryMario.GetComponent<SpriteRenderer>();

        superMarioAnimator = superMario.GetComponent<Animator>();
        smallMarioAnimator = Mario.GetComponent<Animator>();
        fieryMarioAnimator = fieryMario.GetComponent<Animator>();
        
        boxCollider = Mario.GetComponent<BoxCollider2D>();
        
        if(CurrentForm == MarioForms.Fiery){FieryMario();}
        else if(CurrentForm == MarioForms.Super) { SuperMario(); }
        else{ SmallMario(); }
    }
    public static IEnumerator destroyPointsAfterDelay( float delay, GameObject thisObject)
    {

        float elapsedTime = 0f;
        float moveTime = 0.5f;
        Vector3 startPosition = thisObject.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up / 2 ; // move the object up by 1 unit

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveTime); // calculate the interpolation value
            thisObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t); // move the object
            yield return null;
        }
        Destroy(thisObject);
    }
}
