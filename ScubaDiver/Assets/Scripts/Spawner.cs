using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float waitTime;
    public float waitSeconds;
    public GameObject[] Trash;
   
    void Update()
    {

        if (GameManager.Singleton.gameOver)
        {
            this.enabled = false;
        }

        waitTime -= Time.deltaTime;
        int ran = Random.Range(0,Trash.Length);
        if (waitTime <= 1 && Trash != null)
        {
            
            Instantiate(Trash[ran], new Vector3(Random.Range(-8,8),8,0),Quaternion.identity);
            GameManager.Singleton.totalTrash++;
           
            waitTime = waitSeconds;
        }
        //Debug.Log(waitTime);
    }

   

    
}
