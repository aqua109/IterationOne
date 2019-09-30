using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateCube : MonoBehaviour
{
    public void InstantiateCube()
    {
        var cube = PhotonNetwork.InstantiateSceneObject("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0, null);
    }
}
