using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePresist : MonoBehaviour
{
    void Awake()
    {
        int numOfSeanPresist = FindObjectsOfType<ScenePresist>().Length;
        if (numOfSeanPresist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePresist()
    {
        Destroy(gameObject);
    }
}
