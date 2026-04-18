using UnityEngine;
using UnityEngine.UI;

public class PaintModeHUDListener : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject paintHUD;
    [SerializeField] private Image iconDisplay;

    [Header("Icons")]
    [SerializeField] private Sprite squareIcon;
    [SerializeField] private Sprite semicircleIcon;
    [SerializeField] private Sprite triangleIcon;

    private PaintModeController paintController;

    private void Start()
    {
        // HUD starts hidden
        if (paintHUD != null)
            paintHUD.SetActive(false);
    }

    private void Update()
    {
        // Wait until player exists (spawned at runtime)
        if (paintController == null)
        {
            paintController = FindFirstObjectByType<PaintModeController>();
            return;
        }

        bool isInPaintMode = paintController.IsInPaintMode;

        // Toggle HUD
        if (paintHUD != null)
            paintHUD.SetActive(isInPaintMode);

        // Update icon
        if (isInPaintMode)
            UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (iconDisplay == null) return;

        switch (paintController.CurrentType)
        {
            case PaintPrimitiveType.Square:
                iconDisplay.sprite = squareIcon;
                break;

            case PaintPrimitiveType.Semicircle:
                iconDisplay.sprite = semicircleIcon;
                break;

            case PaintPrimitiveType.Triangle:
                iconDisplay.sprite = triangleIcon;
                break;
        }
    }
}