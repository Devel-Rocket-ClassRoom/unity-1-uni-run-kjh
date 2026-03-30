using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;
    public float coyoteTime = 0.1f;
    private float coyoteTimeCounter = 0f;
    private bool isCoyoteActiive = false;

    private int jumpCount = 0; 
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;

    private SpriteRenderer spriteRenderer;
    private HpSystem hpSystem;



    //CircleCollider2D collider2D; 

    private float health = 100f;    
    //MeshRenderer meshRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        
         

        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
         
        //collider2D = GetComponent<CircleCollider2D>();
        //meshRenderer = GetComponent<MeshRenderer>();
        spriteRenderer.enabled = false; // Hide the player sprite at the start

    }

    private void Start()
    {
        spriteRenderer.enabled = true;
        hpSystem = GetComponent<HpSystem>();
    }
     

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        UpdateCoyoteTime();
        if (hpSystem.Invincible() == true)
        {
            if (Time.time % 0.2f < 0.1f)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.2f);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1.0f);
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
        }

    
        //if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        if (Input.GetButtonDown("Fire1")  && jumpCount < 2 )
        {
            jumpCount++;
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            playerAudio.Play();
        }
        else if(Input.GetButtonUp("Fire1") && playerRigidbody.linearVelocity.y > 0)
        {
            playerRigidbody.linearVelocity *= 0.5f;
        }
       

        if(Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Slide", true);
            animator.SetBool("Sliding", true);
            //collider2D.radius = 0.3f;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("Slide", false);
            animator.SetBool("Sliding", false);

        }

        animator.SetBool("Grounded", isGrounded);
    }
    public void OnSlideEnd()
    {
        animator.SetBool("Slide", false);
    }
    public void OnSlidingEnd()
    {
        animator.SetBool("Sliding", false);
        //collider2D.radius = 0.4f;
    }
    public void OnDie()
    {
        if(isDead) return;
        animator.SetTrigger("Die");
        if (deathClip != null)
        {
            playerAudio.PlayOneShot(deathClip);

        }
         
   

        playerAudio.Play();
        playerRigidbody.linearVelocity = Vector2.zero;

        isDead = true;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
         
        GameManager.instance.OnPlayerDead(0);

        Debug.Log("Player has died.");
         
    }

    private void UpdateCoyoteTime()
    {
        if(!isCoyoteActiive) return;
        coyoteTimeCounter += Time.deltaTime;
        if(coyoteTimeCounter > coyoteTime)
        {
            isCoyoteActiive = false;
            isGrounded = false;

        }
    }

    //private void TakeDamage(float damage)
    //{
    //    if (isDead) return;
    //    health -= damage;
    //    Debug.Log($"Player took {damage} damage. Remaining health: {health}");
    //    if (health <= 0)
    //    {
    //        health = 0;
    //        OnDie();
    //    }
    //    //meshRenderer.enabled = !meshRenderer.enabled; // Toggle visibility for damage feedback
    //}   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !isDead)
        {
            OnDie();
           
            GetComponent<System_Energy>()?.OnFall();

        }
        if(other.tag == "Obstacle")
        {
             
            GetComponent<System_Energy>()?.OnDownEnergy();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //    jumpCount = 0;
        //}
        //if (collision.contacts[0].normal.y > 0.5f)
        //{
        //    isGrounded = true;
        //    jumpCount = 0;
        //}

        if(collision.collider == null) return;
        if(collision.collider.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
            isCoyoteActiive = false;
            coyoteTimeCounter = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = false;
        //}
        if(collision.collider.CompareTag("Ground"))
        {
            isCoyoteActiive = true;
            coyoteTimeCounter = 0;
        }
        //isGrounded = false;
    }
}
