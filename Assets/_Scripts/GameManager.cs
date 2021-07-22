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

    public GameObject titleScren;

    private const string MAX_SCORE = "MAX_SCORE";

    private int numberOFLives = 4;
    public List<GameObject> lives;

    //Empiezan las funciones

    //Se invoca al empezar el juego
    private void Start()
    {
        ShowMaxScore();
    }

    /// <summary>
    /// Metodo que inicia la partida
    /// </summary>
    /// <param name="difficulty">Numero entero que indica la dificultad</param>
    public void StartGame(int difficulty)
    {
        //Cambia a inGame y quita la pantalla de inicio
        gameStates = GameStates.inGame;
        titleScren.SetActive(false);

        //Cambia la dificultad cambiando la instasiación de enemigos y las vidas
        spawnRate /= difficulty;
        numberOFLives -= difficulty;

        for (int i = 0; i < numberOFLives; i++)
        {
            lives[i].SetActive(true);
        }

        //Genera a los objetivos
        StartCoroutine(SpawnTarget());

        //Pone el score a 0
        score = 0;
        UpdateScore(0);
    }

    /// <summary>
    /// Genera enemigos
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTarget()
    {
        //Genera los objetivos solo si estas inGame
        while (gameStates == GameStates.inGame)
        {
            //Espera el momento´para generar los onjetivos
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
    /// Muestra por pantalla los puntos maximos que el jugador a conseguido
    /// </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);

        scoreText.text = "MaxScore:\n" + maxScore;
    }

    /// <summary>
    /// Compara si has tenido mejores puntos quela puntuació maxima anterior
    /// </summary>
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, score);
        }
    }

    /// <summary>
    /// Termina la partida
    /// </summary>
    public void GameOver()
    {
        //resta la cantidad de vidas
        numberOFLives--;

        Image heartImage = lives[numberOFLives].GetComponent<Image>();
        var tempColor = heartImage.color;
        tempColor.a = 0.3f;
        heartImage.color = tempColor;

        //Compar si tines 0 vidas y termina el juego
        if (numberOFLives<=0)
        {
            SetMaxScore();
            ShowMaxScore();

            gameStates = GameStates.gameOver;
            gameOver.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Vuelve a iniciar el juego
    /// </summary>
    public void restartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
