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
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ArrowPos = transform.position;
        Vector2 direction = mousePos - ArrowPos;
        transform.right = direction ;

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Singleton.collectedTrash > 3)
            {
                Shoot();
                GameManager.Singleton.collectedTrash = 0;
            }

           
        }
        
        

       /* if(trashBag.transform.GetChildCount() >0)
        {
            GameObject trash0 = trashBag.transform.GetChild(0).gameObject;
            GameObject trash1 = trashBag.transform.GetChild(1).gameObject;
            GameObject trash2 = trashBag.transform.GetChild(2).gameObject;
            bullets[0] = trash0;
            bullets[1] = trash1;
            bullets[2] = trash2;
        }*/

        

    }



   




    /*void Shoot()
    {
        
       

        
        for (int i = 1; i <= 3; i++)
        {
            GameObject newBullet = Instantiate(bullets[i], shotPoint.transform.position, shotPoint.transform.rotation);
            newBullet.AddComponent<Rigidbody2D>();
            
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * Time.deltaTime;
            Debug.Log(i);
        }


    }*/

    void Shoot()
    {
        GameObject bullet = bullets[Random.Range(0, 7)];

        GameObject newBullet = Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
        newBullet.GetComponent<BoxCollider2D>().enabled = false;
        newBullet.GetComponent<Trash>().temp = false;
        // newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce*Time.deltaTime;
        for (int i = 1; i <= 3; i++)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * Time.deltaTime;
        }


    }
}
