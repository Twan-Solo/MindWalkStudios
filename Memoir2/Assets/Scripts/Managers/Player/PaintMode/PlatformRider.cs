using UnityEngine;

/// <summary>
/// Moves the player transform directly to follow moving platforms.
/// </summary>
public class PlatformRider : MonoBehaviour
{
    private CharacterController cc;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            currentPlatform = hit.collider.GetComponent<MovingPlatform>();
        }
        else
        {
            currentPlatform = null;
        }

        if (currentPlatform == null)
        {
            return;
        }

        currentPlatform.Activate();
        Vector3 delta = currentPlatform.FrameDelta;

        if (delta == Vector3.zero)
        {
            return;
        }

        cc.enabled = false;
        transform.position += delta;
        cc.enabled = true;
    }
}