using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformFollower : MonoBehaviour
{
    private CharacterController controller;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        MovingPlatform platform = hit.collider.GetComponent<MovingPlatform>();

        if (platform != null && hit.normal.y > 0.5f)
        {
            currentPlatform = platform;
        }
    }

    private void LateUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformCenter = currentPlatform.transform.position;

            Vector3 targetPosition = new Vector3(
                platformCenter.x,
                transform.position.y,
                platformCenter.z
            );

            Vector3 delta = targetPosition - transform.position;

            controller.Move(delta);
        }

        currentPlatform = null;
    }
}