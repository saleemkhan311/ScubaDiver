using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
   
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
    

    void Start()
    {
        Singleton = this;
    }

    
    void Update()
    {
        Debug.Log(collectedTrash);
        if(totalTrash >=15)
        {
            
        }
    }

    
}
