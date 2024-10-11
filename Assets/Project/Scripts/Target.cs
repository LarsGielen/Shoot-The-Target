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

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingTowardsTarget = true;
    private TextMeshProUGUI[] hitTextOptions;
    private TextMeshProUGUI currentHitText;

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
            DisplayHitPoints();
        }
    }

    private void DisplayHitPoints()
    {
        if (currentHitText != null)
        {
            StopCoroutine(HideHitText());
            currentHitText.text = "";
        }

        currentHitText = hitTextOptions[Random.Range(0, hitTextOptions.Length)];
        currentHitText.text = points.ToString();
        Debug.Log(points.ToString());
        StartCoroutine(HideHitText());
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
