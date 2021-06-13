using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScript : MonoBehaviour
{
       void Update()
    {
        if (Input.anyKeyDown){
            Application.Quit();
        }
    }
}
