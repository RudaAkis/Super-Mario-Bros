using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Rigidbody2D rb;
    int direction;
    float speed;
    GameObject gc;
    GameController GameCtrl;

    
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        GameCtrl = gc.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
        speed = 1.0f;
        StartCoroutine( AnimatePowerUp() );
    }

    void Update()
    {
        rb.velocity = new Vector2(direction * speed, -1.0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GrowSound").GetComponent<AudioSource>().enabled = true;
            Destroy(gameObject);
            GameCtrl.SuperMario();
            GameCtrl.incScore(1000);    
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trigger" ){
            if(direction == 1){direction = -1;}
            else{direction = 1;}
        }    
    }

     private IEnumerator AnimatePowerUp()
    {
        Vector3 currentPosition = transform.localPosition;
        Vector3 goalPosition = currentPosition + Vector3.up * 0.15f;
        yield return MovePowerUp(currentPosition, goalPosition);
    }

    private IEnumerator MovePowerUp(Vector3 from, Vector3 to)
    {
        float elapsed = 0.0f;
        float duration = 0.9f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
