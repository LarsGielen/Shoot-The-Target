using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject bulletHolePrefab;  // Verwijs naar je bullet hole prefab
    [SerializeField] private float bulletLiveTime = 3f; // Bullet live time of 3 seconsd
    private float timeSpawned;

    void Update()
    {
        // Delete bullet if existing to long
        if (Time.time - timeSpawned >= bulletLiveTime){
            Destroy(gameObject);
            Debug.Log("Bullet deleted, lived to long...");
        }
    }

    private void Awake()
    {
        timeSpawned = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Bullet hit something else!");

        // Verwijder de kogel
        Destroy(gameObject);
        
        if (bulletHolePrefab == null) {
            return;
        }

        // Plaats een bullet hole op de locatie van de impact
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(-contact.normal);  // Draai zodat het gat loodrecht op het oppervlak staat
        Vector3 position = contact.point;  // Impactlocatie

        // Instantieer de bullet hole prefab op de juiste plaats en rotatie
        GameObject bulletHole = Instantiate(bulletHolePrefab, position, rotation);
        bulletHole.transform.Rotate(Vector3.forward, Random.Range(0f, 360f), Space.Self);
        bulletHole.transform.SetParent(collision.transform, true);
    }

    public void SetBulleHolePrefab(GameObject bulletHolePrefab)
    {
        this.bulletHolePrefab = bulletHolePrefab;
    }
}
