using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] float bulletMass;
    [SerializeField] float bulletSpeed;
    [SerializeField] int magazineSize;
    [SerializeField] float reloadTime;
    [SerializeField] float delayBetweenShots;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject bulletSpawnPoint;
    private float timeLastShot;
    private int bullets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets <= 0 && Time.time - timeLastShot >= reloadTime){
            bullets = magazineSize;
            UpdateCanvas();
        }
    }

    private void Awake() {
        bullets = magazineSize;
        UpdateCanvas();
    }

    public void shoot()
    {
        // Check delay tussen shots en bullet amount, een van beide niet goed -> return
        if (Time.time - timeLastShot < delayBetweenShots || bullets <= 0) {
            return;
        }

        bullets--;
        timeLastShot = Time.time;
        UpdateCanvas();

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.tag = "Bullet";
        // sphere.layer = LayerMask.NameToLayer("Bullet");
        
        // Stel positie in
        sphere.transform.position = bulletSpawnPoint.transform.position;
        sphere.transform.rotation = bulletSpawnPoint.transform.rotation;
        
        // Pas de schaal aan
        sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.mass = bulletMass;
        rb.velocity = sphere.transform.forward * bulletSpeed;

        Bullet bulletScript = sphere.AddComponent<Bullet>();
        bulletScript.SetBulleHolePrefab(bulletHolePrefab);

        // Verander materiaal of kleur (optioneel)
        Renderer renderer = sphere.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;
    }

    private void UpdateCanvas()
    {
        if (text == null) {
            return;
        }
        
        text.text = bullets + "/" + magazineSize;
    }
}
