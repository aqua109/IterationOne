using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Nickname : MonoBehaviour
{
    
    public TextMeshPro PlayerName;
    // Start is called before the first frame update
    void Start()
    {
        PlayerName.text = "Start()";
    }

    public void updateText()
    {
        PlayerName.text = "Updated";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
