using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PlaySubMenu;
    public GameObject OptionsMenu;

    public void OnPlayButtonClicked()
    {
        MainMenu.SetActive(false);
        PlaySubMenu.SetActive(true);
    }

    public void OnPlayEasyButtonClicked()
    {
        
    }

    public void OnOptionsButtonClicked()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void OnOptionsBackButtonClicked()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OnOptionsSaveButtonClicked()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }


}