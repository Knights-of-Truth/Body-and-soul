 using UnityEngine;
 
 public class MusicClass : MonoBehaviour
 {
     public Object[] myMusic; // declare this as Object array
     private int i;
     private bool paused = false;
     AudioSource _audioSource;
     
     void Awake () {
         DontDestroyOnLoad(transform.gameObject);
         _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = myMusic[0] as AudioClip;
     }
     
     void Start (){
        _audioSource.Play(); 
     }
     void Update () {
        if((!_audioSource.isPlaying && !paused) || Input.GetKeyDown(KeyCode.M))
          playnextSong();
        if(Input.GetKeyDown(KeyCode.P)){
            if(_audioSource.isPlaying){
                paused = true;
                _audioSource.Pause();
            }
            else{
                _audioSource.UnPause();
                paused = false;
            }
        }

     }
     
     void playnextSong() {
         i++;
         if(i == myMusic.Length){
             i=0;
         }
        _audioSource.clip = myMusic[i] as AudioClip;
        _audioSource.Play();
     }
 
 }
