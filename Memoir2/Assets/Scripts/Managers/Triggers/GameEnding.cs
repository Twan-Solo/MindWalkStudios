using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private float transitionDelay = 1f;
    [SerializeField] private FadeTransition fadeTransition;

    private bool m_IsEnding = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_IsEnding)
        {
            m_IsEnding = true;
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        // STOP PLAYER INPUT / ACTIVITY (NOT DESTROY)
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(transitionDelay);

        // FADE FIRST (if available)
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




