using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    private enum AnimStates {idle,walk,jump,fall, pick };
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }

        float horizontal = Input.GetAxis("Horizontal");

        AnimStates state;

        if (horizontal > 0)
        {
            state = AnimStates.walk;
            transform.localScale = new Vector3(0.4f, transform.localScale.y, transform.localScale.z);
            
        }
        else if (horizontal < 0)
        {

            state = AnimStates.walk;
          
            transform.localScale = new Vector3(-0.4f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            state = AnimStates.idle;
        }


        if (rb.velocity.y > .1)
        {
            state = AnimStates.jump;
        }
        else if (rb.velocity.y < -2)
        {
            state = AnimStates.fall;
        }
        /*else
        {
            state = AnimStates.idle;
        }*/

        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }

        anim.SetInteger("animState", (int)state);
      
    }

   
}
