using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;

public class PhotonViewTest : MonoBehaviour, IMixedRealityFocusHandler
{
    public void OnFocusEnter(FocusEventData eventData)
    {
        // ask the photonview for permission
        var photonView = this.GetComponent<PhotonView>();

        photonView?.RequestOwnership();
    }

    public void OnFocusExit(FocusEventData eventData)
    {
    }

    public void cube()
    {
        PhotonNetwork.InstantiateSceneObject("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0, null);
    }
}