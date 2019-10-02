using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreateCube : MonoBehaviour
{
    private int i = 0;

    public void InstantiateCube()
    {
        //PhotonView photonView = PhotonView.Get(this);
        var cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0);
        cube.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
        //photonView.RPC("UpdateText", RpcTarget.All, i);
        i++;
    }

    [PunRPC]
    void UpdateText(int i)
    {
        var cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0);
        cube.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
    }
}
