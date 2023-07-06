using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Canvas backgroundCanvas, loadingScreenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeBackground());
        Invoke("StartGame", 8.0f);
    }
    IEnumerator changeBackground()
    {
        yield return new WaitForSeconds(5.0f);
        backgroundCanvas.enabled = false;
        loadingScreenCanvas.enabled = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene($"{1}-{1}");
    }
}
