using UnityEngine;
using UnityEngine.UI;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using Photon.Pun;

public class SetGroupUsers : MonoBehaviour
{
    //    private static double x_orig = 15.75;
    //    private static double y_orig = 5;
    //    private static double z_orig = 1;

    //    private static double y_off = 1;
    //    private static double z_off = -2;


    //    public void Awake()
    //    {
    //        GameObject groupUserLoader = GameObject.Find("LoadGroupMembers");
    //        GroupMembersConnector connector = (GroupMembersConnector)groupUserLoader.GetComponent(typeof(GroupMembersConnector));

    //        int length = connector.tablets.Length;

    //        double x = x_orig;
    //        double y = y_orig;
    //        double z = z_orig;

    //        for (int i = 0; i < length; i++)
    //        {
    //            if (i == 0)
    //            {
    //                var tablet = PhotonNetwork.Instantiate("SmallTablet", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
    //                var photonView = tablet.GetComponent<PhotonView>();
    //                photonView.RPC("UpdateTablet", RpcTarget.AllBuffered, photonView.ViewID, i);
    //            }
    //            else
    //            {
    //                if (z <= -1)
    //                {
    //                    z = z_orig;
    //                    y += y_off;
    //                }
    //                else
    //                {
    //                    z += z_off;
    //                }
    //                var tablet = PhotonNetwork.Instantiate("SmallTablet", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
    //                var photonView = tablet.GetComponent<PhotonView>();
    //                photonView.RPC("UpdateTablet", RpcTarget.AllBuffered, photonView.ViewID, i);
    //            }
    //        }
    //    }

    //    //public void loadGroupMembers()
    //    //{
    //    //    GameObject groupUserLoader = GameObject.Find("LoadGroupMembers");
    //    //    GroupMembersConnector connector = (GroupMembersConnector)groupUserLoader.GetComponent(typeof(GroupMembersConnector));

    //    //    int length = connector.tablets.Length;

    //    //    double x = x_orig;
    //    //    double y = y_orig;
    //    //    double z = z_orig;

    //    //    for (int i = 0; i < length; i++)
    //    //    {
    //    //        if (i == 0)
    //    //        {
    //    //            var tablet = PhotonNetwork.Instantiate("SmallTablet", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
    //    //            var photonView = tablet.GetComponent<PhotonView>();
    //    //            photonView.RPC("UpdateTablet", RpcTarget.AllBuffered, photonView.ViewID, i);
    //    //        }
    //    //        else
    //    //        {
    //    //            if (z <= -1)
    //    //            {
    //    //                z = z_orig;
    //    //                y += y_off;
    //    //            }
    //    //            else
    //    //            {
    //    //                z += z_off;
    //    //            }
    //    //            var tablet = PhotonNetwork.Instantiate("SmallTablet", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
    //    //            var photonView = tablet.GetComponent<PhotonView>();
    //    //            photonView.RPC("UpdateTablet", RpcTarget.AllBuffered, photonView.ViewID, i);
    //    //        }
    //    //    }
    //    //}

    //    [PunRPC]
    //    void UpdateTablet(int id, int i)
    //    {
    //        GameObject groupUserLoader = GameObject.Find("LoadGroupMembers");
    //        GroupMembersConnector connector = (GroupMembersConnector)groupUserLoader.GetComponent(typeof(GroupMembersConnector));

    //        int length = connector.tablets.Length;

    //        var tablet = PhotonView.Find(id).gameObject;

    //        tablet.transform.Find("Canvas/first_name").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.tablets[i % length].first_name);
    //        tablet.transform.Find("Canvas/last_name").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.tablets[i % length].last_name);
    //        tablet.transform.Find("Canvas/group_number").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.tablets[i % length].group_number.ToString());
    //        tablet.transform.Find("Canvas/email").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.tablets[i % length].email);
    //    }

}