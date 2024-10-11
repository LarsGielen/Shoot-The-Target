using UnityEngine;
using TMPro;
using System.Collections;

public class Target : MonoBehaviour
{
    public enum MovementDirection
    {
        Left,
        Right
    }

    [Header("Movement Settings")]
    [SerializeField] MovementDirection movementDirection;
    [SerializeField] bool isMovableTarget;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float travelDistance = 5.0f;

    [Header("Shooting Settings")]
    [SerializeField] int points = 10;
    [SerializeField] GameObject canvas;

    [Header("Audio Settings")]
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;
     
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingTowardsTarget = true;
    private TextMeshProUGUI[] hitTextOptions;
    private TextMeshProUGUI currentHitText;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("audioSource is null");
        }
    }

    void Start()
    {
        startPosition = transform.position;
        SetInitialTargetPosition();

        hitTextOptions = canvas.GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isMovableTarget)
        {
            MoveTowardsTarget();

            if (HasReachedTarget())
            {
                SwitchDirection();
            }
        }
    }

    private void SetInitialTargetPosition()
    {
        if (movementDirection == MovementDirection.Left)
        {
            targetPosition = startPosition - new Vector3(travelDistance, 0, 0);
        }
        else
        {
            targetPosition = startPosition + new Vector3(travelDistance, 0, 0);
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private bool HasReachedTarget()
    {
        return Vector3.Distance(transform.position, targetPosition) < 0.01f;
    }

    private void SwitchDirection()
    {
        if (movingTowardsTarget)
        {
            targetPosition = startPosition - new Vector3(travelDistance, 0, 0);
        }
        else
        {
            targetPosition = startPosition + new Vector3(travelDistance, 0, 0);
        }

        movingTowardsTarget = !movingTowardsTarget;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.instance.AddScore(points);

            PlayHitSound();
            DisplayHitPoints();
        }
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
