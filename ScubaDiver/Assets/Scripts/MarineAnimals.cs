using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineAnimals : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 30)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            //speed *= -1;
        }
        else if (transform.position.x < -30)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            // speed *= -1;
        }

        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }
}
