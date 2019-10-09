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


    public void Reset()
    {

        foreach (Transform clone in GameObject.Find("Root").transform)
        {
            Destroy(clone.gameObject);
        }

        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));

        int length = connector.cards.Length;
        string list = connector.cards[0].idList;

        double x = x_orig;
        double y = y_orig;
        double z = z_orig;


        for (int i = 0; i < length; i++)
        { 
            if (connector.cards[i % length].idList != "5d93e513f545620b3fa5a35b")
            {
                if (i == 0)
                {
                    var card = PhotonNetwork.Instantiate("Card", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
                    var photonView = card.GetComponent<PhotonView>();
                    photonView.RPC("UpdateCardText", RpcTarget.AllBuffered, photonView.ViewID, i);
                }

                else
                {
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
                    var card = PhotonNetwork.Instantiate("Card", new Vector3((float)x, (float)y, (float)z), Quaternion.identity, 0);
                    var photonView = card.GetComponent<PhotonView>();
                    photonView.RPC("UpdateCardText", RpcTarget.AllBuffered, photonView.ViewID, i);

                }
            }

        }

    }

    [PunRPC]
    void UpdateCardText(int id, int i)
    {
        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));

        int length = connector.cards.Length;

        Debug.Log(connector.cards[i % length].name);


        var card = PhotonView.Find(id).gameObject;
        Renderer label = card.transform.Find("Label").GetComponent<Renderer>();
        Renderer expLabel = card.transform.Find("ExpandedLabel").GetComponent<Renderer>();
        Color newCol;
        if (ColorUtility.TryParseHtmlString(connector.cards[i % length].labels[0].color, out newCol))
        {
            label.materials[0].color = newCol;
            expLabel.materials[0].color = newCol;
        }
        card.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].name);
        card.transform.Find("Expanded/Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].name);
        //card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].idList);
        //card.transform.Find("Expanded/Canvas/Members").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[c % length].idMembers[0].ToString());
        try
        {
            card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].due.ToString());
        }
        catch (NullReferenceException e)
        {
            card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText("No Due Date");
        }
        try
        {
            card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText(connector.cards[i % length].desc);
        }
        catch (NullReferenceException e)
        {
            card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText("No Description");
        }

        if (connector.cards[i % length].idList == "5d54bbb6da4a043bbc645700")
        {
            card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: ToDo");
        }
        if (connector.cards[i % length].idList == "5d54bbb8f51b346c3d9303b7")
        {
            card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: Doing");
        }
        if (connector.cards[i % length].idList == "5d54bbbef54a7c158e8b7f11")
        {
            card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: Done");
        }

        if (card.transform.position[1] < 3.5f || card.transform.position[1] > 7.5f)
        {
            card.SetActive(false);
        }

        //Debug.Log(card.transform.position[i]);
        //Debug.Log(card.transform.position[i].GetType());
        //c++;
    }


    //Coming Soon :)
    public void Scroll()
    {
        foreach (Transform clone in GameObject.Find("Root").transform)
        {
            Vector3 p = clone.transform.position;
            p.y += (float) (6 * (double)GameObject.Find("ThumbRoot").transform.position[1] - 36.375);
            clone.transform.position = p;
            //clone.transform.Translate(Vector3.up * (float)(6 * (double)GameObject.Find("ThumbRoot").transform.position[1] - 36.375));
            //clone.transform.Translate(Vector3.up * GameObject.Find("ThumbRoot").transform.position[1], Space.World);
        }
        Debug.Log((double) GameObject.Find("ThumbRoot").transform.position[1]);
    }

    public void ScrollUp()
    {
        foreach (Transform clone in GameObject.Find("Root").transform)
        {
            Vector3 p = clone.transform.position;
            p.y++;
            clone.transform.position = p;
            DisplayCard(clone);
        }
    }

    public void ScrollDown()
    {
        foreach (Transform clone in GameObject.Find("Root").transform)
        {
            Vector3 p = clone.transform.position;
            p.y--;
            clone.transform.position = p;
            DisplayCard(clone);
        }
    }

    public void DisplayCard(Transform clone)
    {
        if (clone.transform.position[1] < 3.5f || clone.transform.position[1] > 7.5f)
        {
            clone.gameObject.SetActive(false);
        }
        else
        {
            clone.gameObject.SetActive(true);
        }
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