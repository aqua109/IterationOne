using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;


public class SetNickname : MonoBehaviour, IPunInstantiateMagicCallback
{
    private static double x_orig = -1.5;
    private static double y_orig = 3;
    private static double z_orig = 3;

    private static double x_off = 1.5;
    private static double y_off = 0.5;


    public void loadNicknames()
    {
        GameObject nicknameLoader = GameObject.Find("NicknameLoader");
        NicknameConnector connector = (NicknameConnector)nicknameLoader.GetComponent(typeof(NicknameConnector));

        int length = connector.nicknames.Length;

        double x = x_orig;
        double y = y_orig;
        double z = z_orig;

        

        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                var nickname = PhotonNetwork.Instantiate("Nickname", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
                var photonView = nickname.GetComponent<PhotonView>();
                photonView.RPC("UpdateNicknameText", RpcTarget.AllBuffered, photonView.ViewID, i);
            }
            else
            {
                if (x >= 1.5f)
                {
                    x = x_orig;
                    y += y_off;
                }
                else
                {
                    x += x_off;
                }
                var nickname = PhotonNetwork.Instantiate("Nickname", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
                var photonView = nickname.GetComponent<PhotonView>();
                photonView.RPC("UpdateNicknameText", RpcTarget.AllBuffered, photonView.ViewID, i);
            }
        }
    }

    [PunRPC]
    void UpdateNicknameText(int id, int i)
    {
        GameObject nicknameLoader = GameObject.Find("NicknameLoader");
        NicknameConnector connector = (NicknameConnector)nicknameLoader.GetComponent(typeof(NicknameConnector));

        int length = connector.nicknames.Length;

        var nickname = PhotonView.Find(id).gameObject;
        Renderer sphere = nickname.transform.Find("Sphere").GetComponent<Renderer>();
        Color newCol;
        if (ColorUtility.TryParseHtmlString(connector.nicknames[i % length].colour, out newCol))
        {
            sphere.materials[0].color = newCol;
        }
        nickname.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.nicknames[i % length].nickname);
    }

    public void Nickname()
    {
        var photonView = this.GetComponent<PhotonView>();

        Debug.Log(this.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().text);

        var playerID = photonView.Owner.ToString().Substring(2, 1) + "001";

        var player = PhotonView.Find(int.Parse(playerID)).gameObject;

        GameObject nicknameLoader = GameObject.Find("NicknameLoader");
        VRPlayerManager playerManager = (VRPlayerManager)player.GetComponent(typeof(VRPlayerManager));
        playerManager.Nickname(this.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().text, int.Parse(playerID));


        foreach (Transform clone in GameObject.Find("Nicknames").transform)
        {
            PhotonNetwork.Destroy(clone.gameObject);
        }

    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var parent = GameObject.Find("Nicknames");

        this.transform.SetParent(parent.transform, true);
    }

}
