using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float launchForce;
    public GameObject[] bullets;
    public Transform shotPoint;
    public GameObject trashBag;
    
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

            Shoot();
        }
        
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
}
