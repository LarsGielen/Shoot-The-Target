using UnityEngine;

public class MovableTarget : MonoBehaviour
{
    public enum MovementDirection
    {
        Left,
        Right
    }

    [Header("Movement Settings")]
    [SerializeField] MovementDirection movementDirection;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float travelDistance = 5.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingTowardsTarget = true;

    void Start()
    {
        startPosition = transform.position;
        SetInitialTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();

        if (HasReachedTarget())
        {
            SwitchDirection();
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
}
