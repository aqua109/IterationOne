using UnityEngine;
using UnityEngine.UI;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PhotonViewTest : MonoBehaviour
{
    public void cube()
    {
        PhotonNetwork.InstantiateSceneObject("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0, null);
    }
}
