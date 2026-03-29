using UnityEngine;

public class Object_Scrolling : MonoBehaviour
{
    
    public float scrollSpeed = 10f;
    public float returnposX = -25f;

    private PlatformPool platformPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }
    void Start()
    {
        platformPool = FindObjectOfType<PlatformPool>();


        // Update is called once per frame


    }
    void Update()
    {
        //    //if(gameManager != null && !gameManager.isGameOver)
        //    //if(!GameManager.instance.isGameOver)
        //    //{
        //    //    transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);//발판 및 장애물의 이동
        //    //}

        //    if (!GameManager.instance.isGameOver)
        //    {

        //        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        //    }
        //    else
        //    {
        //        // 게임 오버 상태에서는 배경이 멈추도록 설정
        //        transform.Translate(Vector3.zero);
        //    }
        //}
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            if(transform.position.x < returnposX)
            {
                platformPool.ReturnObject(gameObject);
            }



        }
        
    }


        
}
