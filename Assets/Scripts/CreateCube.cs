using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreateCube : MonoBehaviour
{
    private int i = 0;
    public GameObject data;

    public void InstantiateCube()
    {
        //var cube = PhotonNetwork.InstantiateSceneObject("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0, null);
        var cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0);
        cube.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
        Debug.Log(cube.GetType());
        var point = (GameObject)Instantiate(data, new Vector3(0, 7, -16), Quaternion.identity);
        //point.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
        Debug.Log(point.GetType());
        i++;
    }
}
