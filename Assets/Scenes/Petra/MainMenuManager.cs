using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject quitBox;
    [SerializeField] GameObject mainMenuLayout;
    [SerializeField] GameObject settingsLayout;

    private void Start()
    {
        settingsLayout.SetActive(false);
        quitBox.SetActive(false);
    }
    public void ShowQuitBox()
    {
        mainMenuLayout.SetActive(false);
        quitBox.SetActive(true);

    }
    public void QuitGame()
    {
        Debug.Log("Keluar dari game");
        Application.Quit();
    }
}
