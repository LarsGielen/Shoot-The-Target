using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int points = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}
