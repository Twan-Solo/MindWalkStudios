using UnityEngine;

/// <summary>
/// Platform that moves when the player is on it.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 moveDirection;
    private bool activated;
    public Vector3 FrameDelta { get; private set; }
    private Vector3 lastPosition;
    public void Activate()
    {
        activated = true;
    }

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Update()
    {
        if (!activated)
        {
            return;
        }

        lastPosition = transform.position;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        FrameDelta = transform.position - lastPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        activated = false;
    }
}