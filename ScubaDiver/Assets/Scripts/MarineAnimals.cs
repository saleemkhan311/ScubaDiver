using UnityEngine;

public class MarineAnimals : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
        if (Mathf.Abs(transform.position.x) < 30) return;
        var trans = transform;
        var rot = trans.rotation;
        rot = Quaternion.Euler(rot.x, rot.y + 180, rot.z);
        transform.rotation = rot;
        // if (transform.position.x > 30)
        // {
        //     transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        //     //speed *= -1;
        // }
        // else if (transform.position.x < -30)
        // {
        //     transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        //     // speed *= -1;
        // }
        // float increas = Time.time / 200000;
        //
        // if (speed <= 8)
        // {
        //     speed += increas;
        // }
    }

    public void IncreaseSpeed(float value)
    {
        speed += value;
    }
}