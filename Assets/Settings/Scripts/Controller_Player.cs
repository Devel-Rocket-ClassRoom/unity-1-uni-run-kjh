using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int jumpCount = 0; 
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;

    private SpriteRenderer spriteRenderer;

    private bool shoudStopSliding = false;
    
    CircleCollider2D collider2D; 


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (deathClip == null)
        {
            Debug.LogError("Death clip is not assigned in the inspector.");
        }

        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CircleCollider2D>();
        spriteRenderer.enabled = false; // Hide the player sprite at the start

    }

    private void Start()
    {
        spriteRenderer.enabled = true;
    }
     

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        //if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        if(Input.GetButtonDown("Fire1")  && jumpCount < 2 )
        {
            jumpCount++;
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //playerRigidbody.AddForce(new Vector2(0f, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetButtonUp("Fire1") && playerRigidbody.linearVelocity.y > 0)
        {
            playerRigidbody.linearVelocity *= 0.5f;
        }
        //else if(Input.GetMouseButtonUp(0) && playerRigidbody.linearVelocity.y > 0)
        //{
        //    playerRigidbody.linearVelocity =  playerRigidbody.linearVelocity;
        //}

        if(Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Slide", true);
            animator.SetBool("Sliding", true);
            collider2D.radius = 0.2f;
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
        collider2D.radius = 0.5f;
    }
    private void OnDie()
    {
        if(isDead) return;
        animator.SetTrigger("Die");
        playerAudio.PlayOneShot(deathClip);
        //playerAudio.clip = deathClip;

        playerAudio.Play();
        playerRigidbody.linearVelocity = Vector2.zero;

        isDead = true;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;

        GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
        gameManager.OnPlayerDead(0);

        Debug.Log("Player has died.");
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !isDead)
        {
            Debug.Log("Player has collided with a deadly object.");
            OnDie();
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = false;
        //}
        isGrounded = false;
    }
}
