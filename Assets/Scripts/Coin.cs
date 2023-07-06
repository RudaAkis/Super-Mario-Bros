using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( AnimateCoin() );
    }

     private IEnumerator AnimateCoin()
    {
        //Get the current and the end position of the block
        Vector3 currentPosition = transform.localPosition;
        Vector3 goalPosition = currentPosition + Vector3.up * 0.4f;

        yield return MoveCoin(currentPosition, goalPosition);
        yield return MoveCoin(goalPosition, currentPosition);
    }

    private IEnumerator MoveCoin(Vector3 from, Vector3 to)
    {
        float elapsed = 0.0f;
        float duration = 0.3f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
        Destroy(gameObject);
    }
}
