using UnityEngine;

public class Loop_Background : MonoBehaviour
{
    public float width; // Width of the background image 
    private float scrollSpeed = 1.5f; // Speed at which the background scrolls    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        // Get the width of the background image using the SpriteRenderer component
        width = backgroundCollider.size.x;

    }


    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGameOver)
        {
            if(transform.position.x < -width)  
            {
                Reposition();
           
            }
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        }
        else
        {
            // 게임 오버 상태에서는 배경이 멈추도록 설정
            transform.Translate(Vector3.zero);
        }
    }
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;  
    }
}
