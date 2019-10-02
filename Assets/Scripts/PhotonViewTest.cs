using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public class PhotonViewTest : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
{
    int i = 0;

    public void InstantiateCube()
    {
        var cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 6, -16.5f), Quaternion.identity, 0);
        var photonView = cube.GetComponent<PhotonView>();
        photonView.RPC("UpdateText", RpcTarget.All, i, photonView.ViewID);
        i++;
    }

    [PunRPC]
    void UpdateText(int i, int id)
    {
        //use info to find photonview, gameobject.find to find cubeo
        var cube = PhotonView.Find(id).gameObject;
        cube.transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
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