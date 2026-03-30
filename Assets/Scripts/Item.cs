using UnityEngine;

public class Item : MonoBehaviour
{
    public HpSystem hpSystem; // 장애물 충돌 시 HP 감소를 위해 참조
    public enum ItemType
    {
        Coin,
        Obstacle,
        Jelly,
        Potion
    }

    public ItemType itemType = ItemType.Coin; // Inspector에서 타입 선택
    private void Start()
    {
        hpSystem = GetComponent<HpSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            System_Energy energy = other.GetComponent<System_Energy>();

            switch (itemType)
            {
                case ItemType.Coin:
                    GameManager.instance.AddScore(100);
                    break;

                case ItemType.Obstacle:
                    hpSystem.Hit();
                    break;

                case ItemType.Jelly:
                    GameManager.instance.AddScore(1111);
                    break;
                case ItemType.Potion:
                    energy?.OnUpEnergy();
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}

