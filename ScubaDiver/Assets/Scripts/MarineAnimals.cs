using UnityEngine;

public class MarineAnimals : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        var trans = transform;
        // trans.position = trans.right * (speed * Time.deltaTime);
        trans.Translate(speed * Time.deltaTime, 0f, 0f);
        if (Mathf.Abs(trans.position.x) < 30) return;
        var cond = trans.position.x < 0;
        speed *= -1;
        var rot = trans.rotation;
        rot = Quaternion.Euler(rot.x, cond ? 0 : 180, rot.z);
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