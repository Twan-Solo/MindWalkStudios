using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

/// <summary>
/// Raycast version of paint mode, the ghost snaps to a plane below the character
/// </summary>
public class PaintModeControllerRaycast : MonoBehaviour
{
    public bool IsInPaintMode => inPaintMode;
    public PaintPrimitiveType CurrentType => currentType;

    [Header("Spawn Settings")]
    [SerializeField] private int maxPerType = 5;

    [Header("Aim Settings")]
    [SerializeField] private float aimDistance = 10f;
    [SerializeField] private float aimDistanceBelow = 2f;
    [SerializeField] private Transform aimSource;

    [Header("Primitive Prefabs")]
    [SerializeField] private List<PrimitivePrefabEntry> primitiveEntries;

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
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard == null || mouse == null)
        {
            return;
        }

        if (keyboard.qKey.wasPressedThisFrame)
        {
            inPaintMode = !inPaintMode;
            if (inPaintMode)
            {
                SpawnGhost();
            }
            else
            {
                DestroyGhost();
            }
        }

        if (!inPaintMode)
        {
            return;
        }

        UpdateGhostPosition();

        float scroll = mouse.scroll.y.ReadValue();
        if (scroll > 0f)
        {
            CycleShape(1);
        }
        else if (scroll < 0f)
        {
            CycleShape(-1);
        }

        if (mouse.leftButton.wasPressedThisFrame)
        {
            TryPlace();
        }
    }

    // Shoots a ray that detects the horizontal plane below the player
    private bool TryGetAimPoint(out Vector3 hitpoint)
    {
    Plane aimPlane = new Plane(Vector3.up, transform.position + Vector3.down * aimDistanceBelow);
    Ray ray = new Ray(aimSource.position, aimSource.forward);

    float enter = 0f;
    if (aimPlane.Raycast(ray, out enter) && enter <= aimDistance)
    {
        Vector3 rawHit = ray.GetPoint(enter);
        Vector3 playerGround = new Vector3(transform.position.x, rawHit.y, transform.position.z);
        // pulls the placement closer to the player because it felt off
        hitpoint = Vector3.Lerp(playerGround, rawHit, 0.5f);
        return true;
    }

    hitpoint = Vector3.zero;
    return false;
    }
    
    // Handles updating the ghost prefabs position for aiming and hides the ghost if youre not aiming at the plane
    private void UpdateGhostPosition()
    {
        if (currentGhost == null)
        {
            return;
        }

        if (TryGetAimPoint(out Vector3 hitpoint))
        {
            currentGhost.SetActive(true);
            currentGhost.transform.position = hitpoint;
            currentGhost.transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        }

        else
        {
            currentGhost.SetActive(false);
        }
    }

    private void SpawnGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        if (primitiveMap.ContainsKey(currentType) && primitiveMap[currentType].ghostPrefab != null)
        {
            currentGhost = Instantiate(primitiveMap[currentType].ghostPrefab);
            UpdateGhostPosition();
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

       if (!TryGetAimPoint(out Vector3 hitpoint))
        {
            Debug.Log("No valid aim point");
            return;
        }

        // Destroys the ghost when placing and respawns it after
        DestroyGhost();

        Quaternion spawnRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        PrimitivePrefabEntry entry = primitiveMap[currentType];
        GameObject spawned = Instantiate(entry.prefab, hitpoint, spawnRotation);
        
        PlatformLifetime lifetime = spawned.GetComponent<PlatformLifetime>();
        if (lifetime != null)
        {
            lifetime.Setup(this, currentType);
        }
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

    // Handles refunding the platforms
    public void Refund(PaintPrimitiveType type)
    {
        if (placedCounts.ContainsKey(type) && placedCounts[type] > 0)
        {
            placedCounts[type]--;
        }
    }
}