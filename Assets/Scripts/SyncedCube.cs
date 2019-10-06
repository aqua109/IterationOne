using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public class SyncedCube : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
{
    public void InstantiateCube()
    {
        var cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0);
        var photonView = cube.GetComponent<PhotonView>();
        photonView.RPC("UpdateText", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    void UpdateText(int id)
    {
        var cube = PhotonView.Find(id).gameObject;
        cube.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(id.ToString());
    }

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

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var parent = GameObject.Find("Root");

        this.transform.SetParent(parent.transform, true);
    }
}