using UnityEngine;

public class System_Energy : MonoBehaviour
{
    private float maxEnergy = 100f;
    private float decreaseEnergyRate = 10f; // Energy decrease per second    
    private float decreaseObstacleEnergyRate = 20f; // Energy decrease when colliding with an obstacle
    private float increaseEnergyRate = 10f; // Energy increase when collecting an energy item   

    private float currentEnergy;

    public float energyRatio => currentEnergy / maxEnergy;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnergy = maxEnergy; // Start with full energy
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        currentEnergy -= decreaseEnergyRate * Time.deltaTime;

        currentEnergy = Mathf.Max(0f, currentEnergy);

        if (currentEnergy <= 0f)
        {
            // Handle player death or game over logic here
            GameManager.instance.OnPlayerDead(0);
        }
    }

    public void Drain(float amount)
    {
        if(GameManager.instance.isGameOver)  return;
        currentEnergy = Mathf.Max(0f, currentEnergy - amount);

        if(currentEnergy <= 0f)
        {
            GetComponent<Controller_Player>()?.OnDie();
        }
    }

    public void OnHitObstacle()
    {
        currentEnergy -= decreaseObstacleEnergyRate;
        currentEnergy = Mathf.Max(0f, currentEnergy);
    }

    public void OnIncreaseEnergy()
    {
        currentEnergy += increaseEnergyRate;
        currentEnergy = Mathf.Min(maxEnergy, currentEnergy);
    }
    public void OnFallIntoVoid()
    {
        currentEnergy = 0f;
        GameManager.instance.OnPlayerDead(0);
    }
}
