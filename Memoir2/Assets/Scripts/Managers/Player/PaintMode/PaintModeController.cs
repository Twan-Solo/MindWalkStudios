using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// Controls paint mode toggling, shape selection and spawning.
/// </summary>
public class PaintModeController : MonoBehaviour
{
    //AS added 
    public bool IsInPaintMode => inPaintMode;
    public PaintPrimitiveType CurrentType => currentType; 
    //

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnAnchor;
    [SerializeField] private int maxPerType = 5;

    [Header("Primitive Prefabs")]
    [SerializeField] private List<PrimitivePrefabEntry> primitiveEntries;

    // Paint mode visuals - added by Thomas, meant to show the paintbrush and palette when in paint mode, can delete if conflicts with other code
    [Header("Paint Mode Visuals")]
    [SerializeField] private GameObject player_paintbrush;
    [SerializeField] private GameObject player_palette;

    // converts Inspector list into dictionary for quick lookup
    private Dictionary<PaintPrimitiveType, PrimitivePrefabEntry> primitiveMap;
    private Dictionary<PaintPrimitiveType, int> placedCounts;
    private PaintPrimitiveType currentType;
    private bool inPaintMode;

    private PaintPrimitiveType[] allTypes; 
    private int currentTypeIndex;
    private GameObject currentGhost;

    private void Awake()
    {
        primitiveMap = new Dictionary<PaintPrimitiveType, PrimitivePrefabEntry>();
        foreach (PrimitivePrefabEntry entry in primitiveEntries)
        {
            primitiveMap[entry.type] = entry;
        }

        allTypes = (PaintPrimitiveType[])System.Enum.GetValues(typeof(PaintPrimitiveType));
        placedCounts = new Dictionary<PaintPrimitiveType, int>();
        foreach (PaintPrimitiveType type in allTypes)
        {
            placedCounts[type] = 0;
        }

        currentTypeIndex = 0;
        currentType = allTypes[currentTypeIndex];
        currentGhost = null;

        // Paint mode visuals - added by Thomas, meant to show the paintbrush and palette when in paint mode, can delete if conflicts with other code
        if (player_paintbrush != null) player_paintbrush.SetActive(false);
        if (player_palette != null) player_palette.SetActive(false);
    }

    private void SpawnGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        if (primitiveMap.ContainsKey(currentType) && primitiveMap[currentType].ghostPrefab != null)
        {
            currentGhost = Instantiate(primitiveMap[currentType].ghostPrefab, spawnAnchor.position, Quaternion.identity);
        }
    }

    private void DestroyGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
            currentGhost = null;
        }
    }

    private void Update()
    {
        //Debug.Log("Paint Mode: " + inPaintMode);
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        Gamepad gamepad = Gamepad.current;
        if (keyboard == null || mouse == null)
        {
            return;
        }

        // toggle paint mode: Q on keyboard, Y on gamepad
        bool toggled = keyboard.qKey.wasPressedThisFrame ||
                       (gamepad != null && gamepad.buttonNorth.wasPressedThisFrame);

        if (toggled)
        {
            // Toggle paint mode that lets the next part of the code can run
            inPaintMode = !inPaintMode;
            // Paint mode visuals - added by Thomas, meant to show the paintbrush and palette when in paint mode, can delete if conflicts with other code
            if (player_paintbrush != null) player_paintbrush.SetActive(inPaintMode);
            if (player_palette != null) player_palette.SetActive(inPaintMode);

            if (inPaintMode)
            {
                SpawnGhost();
            }
            else
            {
                DestroyGhost();
            }
            Debug.Log("Paint mode toggled: " + inPaintMode);
        }
        // Nothing below this will run unless we're in paint mode, handles placing and cycling between shapes
        if (!inPaintMode)
        {
            return;
        }

        if (currentGhost != null)
        {
            currentGhost.transform.position = spawnAnchor.position;
        }

        float scroll = mouse.scroll.y.ReadValue();
        if (scroll > 0f)
        {
            CycleShape(1);
        }
        else if (scroll < 0f)
        {
            CycleShape(-1);
        }

        if (gamepad != null)
        {
            if (gamepad.rightShoulder.wasPressedThisFrame)
            {
                CycleShape(1);
            }
            else if (gamepad.leftShoulder.wasPressedThisFrame)
            {
                CycleShape(-1);
            }
        }

        bool placed = mouse.leftButton.wasPressedThisFrame ||
                      (gamepad != null && gamepad.leftTrigger.wasPressedThisFrame);

        if (placed)
        {
            TryPlace();
        }
    }

    private void CycleShape(int direction)
    {
        currentTypeIndex = (currentTypeIndex + direction + allTypes.Length) % allTypes.Length;
        currentType = allTypes[currentTypeIndex];
        if (inPaintMode)
        {
            SpawnGhost();
        }
        Debug.Log("Selected shape: " + currentType);
    }

    private void TryPlace()
    {
        // Checks to make sure placing is valid
        if (placedCounts[currentType] >= maxPerType)
        {
            Debug.Log("Max count reached for " + currentType);
            return;
        }
        
        // Destroys the ghost when placing and respawns it after
        DestroyGhost();
        
        PrimitivePrefabEntry entry = primitiveMap[currentType];
        GameObject spawned =Instantiate(entry.prefab, spawnAnchor.position, Quaternion.identity);
        
        MovingPlatform mover = spawned.GetComponent<MovingPlatform>();
        if (mover != null)
        {
            Vector3 facing = transform.forward;
            facing.y = 0f;
            mover.Initialize(facing);
        }
        placedCounts[currentType]++;
        SpawnGhost();
    }
}