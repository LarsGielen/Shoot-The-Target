using System;
using System.Collections;
using System.Collections.Generic;
using AssemblySystem;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    [SerializeField] InputActionReference shootActionLeft;
    [SerializeField] InputActionReference shootActionRight;
    [SerializeField] Weapon weaponLeft;
    [SerializeField] Weapon weaponRight;
    GrabbedManager grabbedManager;
    
    // Start is called before the first frame update
    private void OnEnable() {
        grabbedManager.OnGrab += OnGrab;
        grabbedManager.OnRelease += OnRelease;
    }

    private void OnGrab((GameObject gameObject, bool left) grabbedObject)
    {
        Weapon weapon = grabbedObject.gameObject.GetComponent<Weapon>();

        if (weapon == null){
            return;
        }

        if (grabbedObject.left) {
            weaponLeft = weapon;
        }
        else {
            weaponRight = weapon;
        }
    }

    private void OnRelease((GameObject gameObject, bool left) releasedObject)
    {
        if (releasedObject.left) {
            weaponLeft = null;
        }
        else {
            weaponRight = null;
        }
    }

    private void Awake() {
        grabbedManager = FindObjectOfType<GrabbedManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(shootActionLeft.action.ReadValue<float>());
        if (shootActionLeft.action.ReadValue<float>() >= 0.7f && weaponLeft != null){
            Debug.Log("shoot left");
            weaponLeft.shoot();
        }
        if (shootActionRight.action.ReadValue<float>() >= 0.7f && weaponRight != null){
            Debug.Log("shoot right");
            weaponRight.shoot();
        }
    }
}
