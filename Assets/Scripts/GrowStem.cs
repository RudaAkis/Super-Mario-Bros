using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStem : MonoBehaviour
{
    public float speed = 0.1f;
    void Start()
    {
        StartCoroutine(destroyAfterDelay(9.0f));
    }
    void Update()
    {
        MoveUp();
    }
    IEnumerator destroyAfterDelay( float delay)
    {
        yield return new WaitForSeconds( delay ) ;
        Destroy(gameObject);
    }
    public void MoveUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
