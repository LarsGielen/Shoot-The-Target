using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWeaponUI : MonoBehaviour
{
    [System.Serializable]
    private struct WeaponButton  {
        public Button Button; 
        public GameObject WeaponPrefab;
    }

    [SerializeField] private List<WeaponButton> weaponButtons;

    private void Awake() {
        foreach (WeaponButton weaponButton in weaponButtons) {
            weaponButton.Button.onClick.AddListener(() => Instantiate(weaponButton.WeaponPrefab, this.transform.position, this.transform.rotation));
        }
    }
}
