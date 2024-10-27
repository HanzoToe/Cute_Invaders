using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class MainMenuManager : MonoBehaviour
{
    //if you wanted to edit sounds in menu, but i don't have time for that right now
    //public GameObject SettingsScreen;
   

    private void Awake()
    {

    }

    private void Update()
    {

       
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {

    }
    public void CloseSettingsMenu()
    {
        
    }
}
