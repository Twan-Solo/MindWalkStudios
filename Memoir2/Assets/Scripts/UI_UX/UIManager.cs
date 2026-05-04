using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class Menu
    {
        public string name;
        public GameObject panel;
        public GameObject firstSelected;
        public CanvasGroup canvasGroup;
    }

    public List<Menu> menus = new List<Menu>();

    private Menu currentMenu;

    public void OpenMenu(string menuName)
    {
        Menu target = menus.Find(m => m.name == menuName);
        if (target == null) return;

        SwitchMenu(target);
    }

    private void SwitchMenu(Menu target)
    {
        // LOCK ALL MENUS
        foreach (var menu in menus)
        {
            bool isActive = (menu == target);

            menu.canvasGroup.interactable = isActive;
            menu.canvasGroup.blocksRaycasts = isActive;

            // optional visual hide
            menu.canvasGroup.alpha = isActive ? 1 : 0;
        }

        currentMenu = target;

        // CRITICAL: reset selection
        EventSystem.current.SetSelectedGameObject(null);

        StartCoroutine(SetFocus(target.firstSelected));
    }

    private IEnumerator SetFocus(GameObject obj)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(obj);
    }
}
