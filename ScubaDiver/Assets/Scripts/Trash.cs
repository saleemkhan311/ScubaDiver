using UnityEngine;

public class Trash : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool temp = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }


        if (transform.position.y >= -3)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }


        //if(temp)
        Constraint();
    }

    public void DisableTrash()
    {
        Destroy(rb);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void EnableTrash()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().enabled = true;
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


    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if(collision.collider.CompareTag("Player"))
    //     {
    //         if(GameManager.Singleton.collectedTrash <=2)
    //         {
    //            
    //                 GameManager.Singleton.collectedTrash++;
    //                 GameManager.Singleton.totalTrash--;
    //                 Destroy(rb);
    //                 //transform.position = collision.collider.transform.position;
    //                 GetComponent<BoxCollider2D>().enabled = false;
    //             
    //             
    //         }
    //     }
    // }

    public bool thrown = false;
    public void Throw(Vector2 force)
    {
        thrown = true;
        transform.SetParent(null);
        EnableTrash();
        rb.velocity = force;
        Invoke(nameof(ResetPick), 3f);
    }

    private void ResetPick()
    {
        thrown = false;
    }
    
}