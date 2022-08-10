using UnityEngine;

public class TrashEater : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider == null) return;
        if (!col.collider.CompareTag("Trash")) return;
        Destroy(col.gameObject);
        GameManager.Singleton.TotalTrash--;
        GameManager.Singleton.Score++;
    }
}