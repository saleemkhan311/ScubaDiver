using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed =10;
    public float jumpForce;
    
    float isJumping;
    float vertical;
    float horizontal;
    [SerializeField] LayerMask jumpableGround;
    [SerializeField] LayerMask jumpableSubmarine;
    SpriteRenderer sprite;
    BoxCollider2D collider;
    Animator anim;
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame

    private void Update()
    {

       

        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }


        if (transform.position.x > 8.5)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }
        else if (transform.position.x < -8.5)
        {
            transform.position = new Vector2(-8.5f, transform.position.y);
        }

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2(horizontal * moveSpeed, rb.velocity.y);
        
        if((Input.GetButtonDown("Jump") && isGrounded()) ||(Input.GetButtonDown("Jump") && isGrounded2()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce) ;
            
        }
        

       

        bool isGrounded()
        {
            return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down,0.1f,jumpableGround);
        }
        bool isGrounded2()
        {
            return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableSubmarine);
        }



    }
    
}
