using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public class HpSystem : MonoBehaviour
{
    public Slider hpSlider;          // Inspector에서 슬라이더를 연결해 주세요.
    public float targetHPPercentage = 1f; // 목표 체력 비율 (0.0 ~ 1.0)
    public float fillSpeed = 5f;  
    public float consumeHp;
    public float hitTime;
    public float hitEff;
    public int hitCount;

    private bool hitCheck;
    public Controller_Player player;
    private void Start()
    {
        consumeHp = 0;
        hitCount = 0;
        hitCheck = false;
    }
    void Update()
    {
        // 실제 슬라이더의 값을 목표값으로 부드럽게 이동시킴
        hpSlider.value = Mathf.Lerp(hpSlider.value, targetHPPercentage, Time.deltaTime * fillSpeed);
        consumeHp += Time.deltaTime;
        if(consumeHp >1)  //초당1%씩 체력을 깎음
        {
            TakeDamage(0.01f);
            consumeHp = 0;
        }
        if(hitCheck)   //장애물에 부딛혔는지 확인
        {

            TakeDamage(0.1f);  //장애물에 부딛혔으면 10%체력을깎음
            hitCheck = false;
        }
        
    }

    public void TakeDamage(float damagePercentage)
    {
        if (targetHPPercentage <= 0f) return;

        targetHPPercentage -= damagePercentage;
        targetHPPercentage = Mathf.Clamp(targetHPPercentage, 0f, 1f); // 0~1 사이로 고정
        
    }
    public void Hit() // 장애물에 부딛혔을때
    {
        hitCheck = true;
        hitCount++;
    }
}
