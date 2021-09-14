using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    
    public Animator transition;
    [SerializeField] private float transitionTime = 1f;
    
    [HideInInspector] public int levelIndexData;

    public void LoadNextLevel(){
        levelIndexData = SceneManager.GetActiveScene().buildIndex + 1;
        SaveSystem.savePlayer(this);
        StartCoroutine(LoadLevel(levelIndexData));
    }
    
    IEnumerator LoadLevel(int levelIndex){

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }

    public void ReloadLevel(){

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void loadFromSave(){
            PlayerData data = SaveSystem.LoadPlayer();
            levelIndexData = data.level;
            if(levelIndexData == 11){
                levelIndexData = 1;
            }
            if(levelIndexData != 1)
                StartCoroutine(LoadLevel(levelIndexData));
        
    }

}
