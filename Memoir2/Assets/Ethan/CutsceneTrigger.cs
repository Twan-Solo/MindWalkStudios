using UnityEngine;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private UnityEvent _events;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            _events.Invoke();
        }
    }

}
