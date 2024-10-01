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
        // Controleer of het object de "Target" tag heeft
        if (collision.gameObject.CompareTag("Target")) {
            Debug.Log("Bullet hit a target!");

            // Voer logica uit voor targets (bijv. schade toebrengen)

            // Verwijder de kogel zelf
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Terrain")) {
            Debug.Log("Bullet hit something else!");

            // Verwijder de kogel
            Destroy(gameObject);
            
            if (bulletHolePrefab == null) {
                return;
            }

            // Plaats een bullet hole op de locatie van de impact
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.LookRotation(contact.normal);  // Draai zodat het gat loodrecht op het oppervlak staat
            Vector3 position = contact.point + contact.normal*0.01f;  // Impactlocatie

            // Instantieer de bullet hole prefab op de juiste plaats en rotatie
            Instantiate(bulletHolePrefab, position, rotation);
        }
        else {
            // Do nothing
        }
    }

    public void SetBulleHolePrefab(GameObject bulletHolePrefab)
    {
        this.bulletHolePrefab = bulletHolePrefab;
    }
}
