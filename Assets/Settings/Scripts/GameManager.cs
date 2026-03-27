using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;

    public bool isGameOver { get; private set; }

    private int score;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
             
        }
    }

    public void AddScore(int newScore)
    {
        if (isGameOver) return;
        score += newScore;
        scoreText.text = $"Score: {score}";
    }

    public void OnPlayerDead(int newScore)
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }
}
