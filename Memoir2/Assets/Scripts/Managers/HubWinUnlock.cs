using UnityEngine;

public class HubWinUnlock : MonoBehaviour
{
    [Header("Object to Reveal")]
    public GameObject endObject;

    private void Start()
    {
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (endObject == null) return;

        if (PlayerData.Instance != null && PlayerData.Instance.IsGameComplete())
        {
            Debug.Log("ALL LEVELS COMPLETE END OBJECT UNLOCKED");
            endObject.SetActive(true);
        }
        else
        {
            endObject.SetActive(false);
        }
    }
}
