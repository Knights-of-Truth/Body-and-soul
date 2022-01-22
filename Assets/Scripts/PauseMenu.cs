using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button musicbutton;
    public Sprite off;
    public Sprite on;
    public bool bolo = true;    
    public static PauseMenu PM;
    void Awake (){
            PM = this;
        }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if(GameIsPaused)
                Resume();
            else
                Pause();
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        }

    public void MusicOnOff(){
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(AudioSource a in audios){
            if(a.isPlaying)
                a.Pause();
            else
                a.UnPause();
        }
        if(bolo){
            musicbutton.GetComponent<Image>().sprite = off;
            bolo = !bolo;
        }
        else{
            musicbutton.GetComponent<Image>().sprite = on;
            bolo = !bolo;
        }
    }
    public void onn(){
        if(!bolo){
            musicbutton.GetComponent<Image>().sprite = on;
            bolo = !bolo;
        }
    }
    
    public void Quit(){
        Application.Quit();
    }
 
}
