using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    [SerializeField] float flashTime;
    private float timeFlashStart;
    
    private void Awake() {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Time.time - timeFlashStart > flashTime) {
            gameObject.SetActive(false);
        }
    }

    public void Flash()
    {
        timeFlashStart = Time.time;
        gameObject.SetActive(true);
    }
}
