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
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame

    private void Update()
    {
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
        

        if (horizontal==1)
        {
            sprite.sprite = sprites[2];
            sprite.flipX = false;
        }
        else if(horizontal == -1)
        {
            sprite.sprite = sprites[2];
            sprite.flipX = true;
        }
        else if (rb.velocity.y > .1)
        {
            sprite.sprite = sprites[1];
        }
        else if (rb.velocity.y < -2)
        {
            sprite.sprite = sprites[0];
        }
        else
        {
            sprite.sprite = sprites[0];
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
    public void Constraint()
    {
        
    }
}
