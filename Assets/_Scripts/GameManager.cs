using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        loading,
        inGame,
        gameOver
    }

    public GameStates gameStates;


    public List<GameObject> TargetPrefabs;
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    private int _score;
    private int score {
        set
        {
            _score = Mathf.Clamp(value, 0, 9999);
        }

        get
        {
            return _score;
        }
    }

    public TextMeshProUGUI gameOver;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        gameStates = GameStates.inGame;

        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (gameStates == GameStates.inGame)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0, TargetPrefabs.Count);
            Instantiate(TargetPrefabs[index]);
        }
    }

    /// <summary>
    /// Suma los puntos que has consegido
    /// </summary>
    /// <param name="scoreToAdd">Puntos que has conseguido</param>
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    /// <summary>
    /// Termina la partida
    /// </summary>
    public void GameOver()
    {
        gameStates = GameStates.gameOver;
        gameOver.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void restartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
