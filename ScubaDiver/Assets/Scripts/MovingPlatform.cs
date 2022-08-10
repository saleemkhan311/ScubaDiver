using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    

    SpriteRenderer sprite;
    [SerializeField] float speed;
    Transform target;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {


        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }



        if (transform.position.x>15)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
           
        }
        else if (transform.position.x < -15 )
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
          
        }

        
        transform.Translate(speed*Time.deltaTime,0f,0f);
        
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.collider.tag == "Player")
    //     {
    //         collision.collider.transform.SetParent(transform);
    //     }
    // }
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.collider.tag == "Player")
    //     {
    //         collision.collider.transform.SetParent(null);
    //     }
    // }
}
