using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float launchForce;
    public GameObject[] bullets;
   
    public Transform shotPoint;
    public GameObject trashBag;
    public GameObject trash;
    GameObject newBullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        Vector2 ArrowPos = transform.position;
        Vector2 direction = mousePos - ArrowPos;
        transform.right = direction ;


        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Singleton.collectedTrash >= 3)
            {
                Debug.Log("Shoot");
                Shoot();
                GameManager.Singleton.collectedTrash = 0;
                GameManager.Singleton.score++;
            }

           
        }



        Orientation();
        if (newBullet != null &&  newBullet.transform.position.y >= 15)
        {
            Destroy(newBullet);
        }

    }



   


    void Orientation()
    {
        float x = Input.GetAxis("Horizontal");
        if (x > 0)
        {
            
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        }
        else if (x < 0)
        {

            

            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    

    void Shoot()
    {
         GameObject bullet = bullets[Random.Range(0, 7)];

         newBullet = Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);

        newBullet.GetComponent<BoxCollider2D>().enabled = false;
        //newBullet.GetComponent<Trash>().temp = false;
        newBullet.name = "Trash";
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * Time.deltaTime;
        
        

    }
}
