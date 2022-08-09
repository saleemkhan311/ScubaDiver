using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineAnimals : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float speedIncrease;
    public float maxSpeed;
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

        float increase = Time.time / speedIncrease;

        if (speed <= maxSpeed)
        {
            speed += increase;
        }
       
        Debug.Log(increase);

        transform.Translate((speed * Time.deltaTime) + increase, 0f, 0f);
    }
}
