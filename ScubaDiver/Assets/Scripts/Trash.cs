using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
     public bool temp = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.y >= -3)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            
        }

        
        if(temp)
            Constraint();
    }

    void Constraint()
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


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            if(GameManager.Singleton.collectedTrash <4)
            {
                GameManager.Singleton.collectedTrash++;
                GameManager.Singleton.totalTrash--;
                Destroy(rb);
                //transform.position = collision.collider.transform.position;
                GetComponent<BoxCollider2D>().enabled = false;
                
            }
        }
    }
}
