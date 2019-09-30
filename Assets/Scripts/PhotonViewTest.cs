using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;

public class PhotonViewTest : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
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

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        var photonView = this.GetComponent<PhotonView>();

        photonView?.RequestOwnership();
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
    }

    //public void cube()
    //{
    //    PhotonNetwork.InstantiateSceneObject("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0, null);
    //}

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var parent = GameObject.Find("Root");

        this.transform.SetParent(parent.transform, true);
    }
}