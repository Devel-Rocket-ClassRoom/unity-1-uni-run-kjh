using UnityEngine;

public class Item : MonoBehaviour
{

    public enum ItemType
    {
        Coin,
        Obstacle
    }

    public ItemType itemType = ItemType.Coin; // Inspector에서 타입 선택

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            System_Energy energy = other.GetComponent<System_Energy>();

            switch (itemType)
            {
                case ItemType.Coin:
                    GameManager.instance.AddScore(100);
                    energy?.OnUpEnergy();
                    break;

                case ItemType.Obstacle:
                    energy?.OnDownEnergy();
                    break;

                
            }

            gameObject.SetActive(false);
        }
    }
}

