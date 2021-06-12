using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;

    private void Start() {
        Invoke ("changeText", 7f);
    }

    private void changeText(){
        textMesh.text = " ";
    }
}
