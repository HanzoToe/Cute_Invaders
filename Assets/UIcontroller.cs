using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIcontroller : MonoBehaviour

   
{
    AudioManagerScript AudioManagerScript;
    public Slider _musicSlider, _sfxSlider;
    public void Start()
    {
        AudioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }
    public void ToggleMusic()
    {
        AudioManagerScript.Instance.Togglemusic();
    }
    
}
