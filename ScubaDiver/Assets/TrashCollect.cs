using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollect : MonoBehaviour
{
    public Transform trashBag;
    public Gun gun;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }


        if (Input.GetMouseButton(0))
        {
            destroyChild();
        }
   
    }

    void destroyChild()
    {
        if (trashBag.transform.childCount>= 3)
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
         
            if(GameManager.Singleton.collectedTrash <=2)
            {
                     anim.SetBool("isPicking", true);

                    collision.collider.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                    collision.collider.transform.position = trashBag.position;
                    collision.collider.transform.SetParent(trashBag);
                
                    Debug.Log("Working");
                    //gun.bullet = collision.collider.gameObject;
                    

                    StartCoroutine(timer(.5f));
                
            }

        }
    }


    IEnumerator timer(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isPicking", false);
    }

}
