using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerController : MonoBehaviour
{

    GameObject gc;
    GameController GameCtrl;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        GameCtrl = gc.GetComponent<GameController>();

        StartCoroutine( AnimatePowerUp() );
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && GameCtrl.superMarioRenderer.enabled == true)
        {
            Destroy(gameObject);
            GameCtrl.FieryMario();
        }
        else
        {
            Destroy(gameObject);
            GameCtrl.SuperMario();
        }
    }

     private IEnumerator AnimatePowerUp()
    {
        //Get the current and the end position of the block
        Vector3 currentPosition = transform.localPosition;
        Vector3 goalPosition = currentPosition + Vector3.up * 0.05f;
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
        //Destroy(gameObject);
    }
}
