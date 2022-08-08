using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public bool gameOver;
    public static GameManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
                return;
            }
            Destroy(value.gameObject);
        }
            

    }

    public int totalTrash;
    public int collectedTrash;
    public int score;
    

    void Start()
    {
        Singleton = this;
        gameOver = false;
    }

    
    void Update()
    {
        Debug.Log("Collect: "+collectedTrash);
        if(totalTrash >=15)
        {
            gameOver = true;
        }
    }

    
}
