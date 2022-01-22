 using UnityEngine;
 using UnityEngine.UI;
 
 public class MusicClass : MonoBehaviour
 {
    public Object[] myMusic; // declare this as Object array
    private int i;
    AudioSource _audioSource;
    public Sprite on;
     
     void Awake () {
         DontDestroyOnLoad(transform.gameObject);
         _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = myMusic[0] as AudioClip;
     }
     
     void Start (){
        _audioSource.Play(); 
        
     }
     void Update () {
        if(Input.GetKeyDown(KeyCode.M))
          playnextSong();
        
     }
     void playnextSong() {
         PauseMenu.PM.onn();

         i++;
         if(i == myMusic.Length){
             i=0;
         }

        _audioSource.clip = myMusic[i] as AudioClip;
        _audioSource.Play();
        
        Invoke("check", _audioSource.clip.length-1);

        }
        void check(){
           if(PauseMenu.PM.bolo){
              Invoke("playnextSong", 1);
           }
        }
 }
