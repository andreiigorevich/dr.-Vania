using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] private int _playerLives = 3;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _score = 0;


    void Awake()
    {
        int numOfSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _livesText.text = _playerLives.ToString();
        _scoreText.text = _score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (_playerLives > 1)
        {
            Invoke("TakeLive",1.5f);
        }
        else 
        {
            Invoke("ResetGameSession", 1.5f);
        }
    }

    public void AddScore(int pointsToAdd)
    {
        _score += pointsToAdd;
        _scoreText.text = _score.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePresist>().ResetScenePresist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLive()
    {
        _playerLives--;
        int _currentSccenIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSccenIndex);
        _livesText.text = _playerLives.ToString();
    }

}
