using UnityEngine;
using TMPro;
using System.Collections;

public class Target : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] int points = 10;
    [SerializeField] GameObject canvas;

    [Header("Audio Settings")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioSource audioSource;

    private TextMeshProUGUI[] hitTextOptions;
    private TextMeshProUGUI currentHitText;

    void Start()
    {
        hitTextOptions = canvas.GetComponentsInChildren<TextMeshProUGUI>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            HitTarget();
    }

    public void HitTarget() {
        ScoreManager.instance.AddScore(points);
        PlayHitSound();
        DisplayHitPoints();
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    private void DisplayHitPoints()
    {
        if (currentHitText != null)
        {
            StopCoroutine(HideHitText());
            StopCoroutine(AnimateHitText(currentHitText));
            currentHitText.text = "";
        }

        currentHitText = hitTextOptions[Random.Range(0, hitTextOptions.Length)];
        currentHitText.text = points.ToString();
        StartCoroutine(AnimateHitText(currentHitText));
        StartCoroutine(HideHitText());
    }

    private IEnumerator AnimateHitText(TextMeshProUGUI hitText)
    {

        Color originalColor = hitText.color;
        Vector3 originalScale = hitText.transform.localScale;

        hitText.transform.localScale = originalScale * 1.5f;
        hitText.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        hitText.transform.localScale = originalScale;
        hitText.color = originalColor;
    }


    private IEnumerator HideHitText()
    {
        yield return new WaitForSeconds(2);
        if (currentHitText != null)
        {
            currentHitText.text = "";
        }
    }
}
