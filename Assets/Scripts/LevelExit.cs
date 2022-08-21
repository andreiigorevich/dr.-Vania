using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private int _indexOfScene;

    private void Start()
    {
        _indexOfScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        FindObjectOfType<ScenePresist>().ResetScenePresist();
        SceneManager.LoadScene(_indexOfScene + 1);
    }
}
