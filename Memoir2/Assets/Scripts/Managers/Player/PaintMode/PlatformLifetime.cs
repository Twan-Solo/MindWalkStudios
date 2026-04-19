using UnityEngine;

/// <summary>
/// Adds a timer to the platforms destroys them and then refunds them
/// </summary>
public class PlatformLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;

   private PaintModeControllerRaycast controller;
   private PaintPrimitiveType myType;

   public void Setup(PaintModeControllerRaycast owner, PaintPrimitiveType type)
   {
       controller = owner;
       myType = type;
   }
   private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnDestroy()
    {
        if (controller != null)
        {
            controller.Refund(myType);
        }
    }
}
