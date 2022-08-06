using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollect : MonoBehaviour
{
    public Transform trashBag;
    public Gun gun;

    private void Update()
    {

   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Trash" && GameManager.Singleton.collectedTrash <=3)
        {
            collision.collider.transform.position = trashBag.position;
            collision.collider.transform.SetParent(trashBag);
            //gun.bullet = collision.collider.gameObject;

        }
    }
}
