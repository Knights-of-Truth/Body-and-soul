using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private float textTime;

    private void Start() {
        Invoke ("changeText", textTime);
    }

    private void changeText(){
        textMesh.text = " ";
    }
}
