using UnityEngine;
using UnityEngine.UI;

public class UIPanelToggle : MonoBehaviour
{
    [Header("Panel to Control")]
    public GameObject panel;

    [Header("Buttons")]
    public Button showButton;
    public Button hideButton;

    void Start()
    {
        if (showButton != null)
            showButton.onClick.AddListener(ShowPanel);

        if (hideButton != null)
            hideButton.onClick.AddListener(HidePanel);
    }

    public void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);
    }

    public void HidePanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}