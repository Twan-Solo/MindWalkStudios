using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private float transitionDelay = 1f;
    [SerializeField] private FadeTransition fadeTransition;

    private bool isEnding = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnding)
        {
            isEnding = true;
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        Debug.Log("Game Ending Triggered");

        // -------------------------
        // STOP GAMEPLAY INPUT CLEANLY
        // -------------------------

        // Disable player input system safely
        PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        // Unlock cursor BEFORE leaving scene
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Ensure time is normal (prevents weird UI bugs)
        Time.timeScale = 1f;

        yield return new WaitForSeconds(transitionDelay);

        // -------------------------
        // LOAD CREDITS
        // -------------------------

        if (fadeTransition != null)
        {
            fadeTransition.FadeToScene(creditsSceneName);
        }
        else
        {
            SceneManager.LoadScene(creditsSceneName);
        }
    }
}




