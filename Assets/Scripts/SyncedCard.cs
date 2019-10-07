using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;
using Newtonsoft.Json;
using System;

public class SyncedCard : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
{
    public static int c;
    private static double x_orig = -2.6875;
    private static double y_orig = 7.125;
    private static double z_orig = 16.875;

    private static double x_off = 3;
    private static double y_off = -1;


    public void Test()
    {
        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));

        int length = connector.cards.Length;
        string list = connector.cards[0].idList;

        double x = x_orig;
        double y = y_orig;
        double z = z_orig;

        Debug.Log(x_orig);
        Debug.Log(x);
        Debug.Log(x_off);

        for (int i = 0; i < length; i++)
        {
            var card = PhotonNetwork.Instantiate("Card", new Vector3((float) x, (float) y, (float) z), Quaternion.identity, 0);

            var photonView = card.GetComponent<PhotonView>();
            photonView.RPC("UpdateCardText", RpcTarget.AllBuffered, photonView.ViewID, i);

            //Debug.Log(list);
            //Debug.Log(connector.cards[i % length].idList);
            //Debug.Log(list == connector.cards[i % length].idList);

            if (list == connector.cards[i % length].idList)
            {
                y = y + y_off;
            }
            else
            {
                list = connector.cards[i % length].idList;
                y = y_orig;
                x = x + x_off;
            }
            //c++;
        }

    }


    public void InstantiateCard()
    {
        //var card = PhotonNetwork.Instantiate("Card", new Vector3(x_orig, y_orig, z_orig), Quaternion.identity, 0);
        //var photonView = card.GetComponent<PhotonView>();
        //photonView.RPC("UpdateCardText", RpcTarget.AllBuffered, photonView.ViewID);
        //Debug.Log(c);
        Test();
        //c++;
        //GameObject trelloLoader = GameObject.Find("TrelloLoader");
        //TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));
        //connector.GetComponent<TrelloConnector>().Test();
        //Debug.Log(connector.cards[0].name);
    }

    [PunRPC]
    void UpdateCardText(int id, int i)
    {
        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));

        int length = connector.cards.Length;

        //Debug.Log("*************");
        Debug.Log(connector.cards[i % length].name);
        ////Debug.Log(connector.cards[i % length].idList);
        ////Debug.Log(connector.cards[i % length].idMembers[0].ToString());
        ////Debug.Log(connector.cards[i % length].labels[0].color);
        ////Debug.Log(connector.cards[i % length].due.ToString());
        ////Debug.Log(connector.cards[i % length].desc);
        ////Debug.Log(connector.cards.Length);

        //Debug.Log(c);
        //Debug.Log("*************");


        var card = PhotonView.Find(id).gameObject;
        Renderer renderer = card.transform.Find("Label").GetComponent<Renderer>();
        Color newCol;
        if (ColorUtility.TryParseHtmlString(connector.cards[i % length].labels[0].color, out newCol))
        {
            renderer.materials[0].color = newCol;
        }
        card.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].name);
        card.transform.Find("Expanded/Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].name);
        card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].idList);
        //card.transform.Find("Expanded/Canvas/Members").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[c % length].idMembers[0].ToString());
        try
        {
            card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].due.ToString());
            card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].desc);
        }
        catch (NullReferenceException e)
        {
            card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText("No Due Date");
            card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText("No Description");
        }
        //c++;
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