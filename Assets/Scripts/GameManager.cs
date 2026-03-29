using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;

    public Slider energySlider;
    //public Image energyFillImage;
    public TextMeshProUGUI energyText;
    public System_Energy energySystem;

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

        if (!isGameOver && energySystem != null && energySlider != null && energyText != null)
        {
            float ratio = energySystem.energyRatio;
            energySlider.value = ratio;
            energyText.text = $"Energy: {Mathf.CeilToInt(ratio * 100)}";
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
