using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollect : MonoBehaviour
{
    public Transform trashBag;
    public Gun gun;

    private void Update()
    {

        if(Input.GetMouseButton(0))
        {
            destroyChild();
        }
   
    }

    void destroyChild()
    {
        if (transform.childCount >= 3)
        {
            foreach (Transform child in trashBag.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Trash" )
        {
         
            if(GameManager.Singleton.collectedTrash <=3)
            {
                collision.collider.transform.position = trashBag.position;
                collision.collider.transform.SetParent(trashBag);
                //gun.bullet = collision.collider.gameObject;
            }

        }
    }

    
}
