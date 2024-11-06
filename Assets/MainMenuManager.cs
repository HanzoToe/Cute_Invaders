using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class MainMenuManager : MonoBehaviour
{
    //if you wanted to edit sounds in menu, but i don't have time for that right now
    //public GameObject SettingsScreen;
    AudioManagerScript AudioManagerscript;

    private void Awake()
    {
        AudioManagerscript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        
    }
    private void Start()
    {
        AudioManagerscript.Instance.PlayMusic("Theme1");
        AudioManagerscript.Instance.StopSFX("JelloJiggle");
        AudioManagerscript.Instance.StopSFX("Shoot1");
        AudioManagerscript.Instance.StopSFX("Shoot2");

    }

    private void Update()
    {

       
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        AudioManagerscript.Instance.StopMusic("Theme1");
        AudioManagerscript.Instance.StopSFX("JelloJiggle");
        AudioManagerscript.Instance.StopSFX("Shoot1");
        AudioManagerscript.Instance.StopSFX("Shoot2");
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
