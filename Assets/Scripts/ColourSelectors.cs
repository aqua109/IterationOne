using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;
using Newtonsoft.Json;
using System;
using System.Linq;

public class ColourSelectors : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
{
    public Card[] cards;
    private static double x_orig = -0.1875;
    private static double y_orig = 5.875;
    private static double z_orig = -16.25;

    private static double y_off = -0.5;

    public void loadTasks(string name)
    {
        if (GameObject.Find("TrelloCardSelectorText") != null)
        {
            GameObject.Find("TrelloCardSelectorText/TrelloSorterPlaceholder").GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (Transform clone in GameObject.Find("GraphPoints").transform)
        {
            PhotonNetwork.Destroy(clone.gameObject);
        }

        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));
        cards = connector.cards;
        var query = from Card card in cards
                    where card.labels[0].name == name
                    && card.idList != "5d93e513f545620b3fa5a35b"
                    select card;

        

        double x = x_orig;
        double y = y_orig;
        double z = z_orig;
        int i = 0;

        if ((from c in query select c.name).ToList().Count == 0)
        {
            var colourLabel = GameObject.Find("TrelloCardSelectorText");
            GameObject.Find("TrelloCardSelectorText/TrelloSorterPlaceholder").GetComponent<MeshRenderer>().enabled = true;
            colourLabel.transform.Find("TrelloSorterPlaceholder").GetComponentInChildren<TextMeshPro>().SetText("No cards with this colour label");
        }
        else
        {
            foreach (Card c in query)
            {
                var card = PhotonNetwork.Instantiate("SmallCard1", new Vector3((float)x, (float)y, (float)z), Quaternion.Euler(0f, 180f, 0f), 0);
                var photonView = card.GetComponent<PhotonView>();
                photonView.RPC("UpdateCardText", RpcTarget.AllBuffered, photonView.ViewID, c.id);

                y += y_off;
                i++;
            }
        }

    }


    [PunRPC]
    void UpdateCardText(int id, string cardID)
    {
        GameObject trelloLoader = GameObject.Find("TrelloLoader");
        TrelloConnector connector = (TrelloConnector)trelloLoader.GetComponent(typeof(TrelloConnector));
        cards = connector.cards;

        var query = from Card c in cards
                    where c.id == cardID
                    select c;

        foreach (Card c in query)
        {
            Debug.Log(c.name);


            var card = PhotonView.Find(id).gameObject;
            Renderer label = card.transform.Find("Label").GetComponent<Renderer>();
            Renderer expLabel = card.transform.Find("ExpandedLabel").GetComponent<Renderer>();
            Color newCol;
            if (ColorUtility.TryParseHtmlString(c.labels[0].color, out newCol))
            {
                label.materials[0].color = newCol;
                expLabel.materials[0].color = newCol;
            }
            card.transform.Find("Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(c.name);
            card.transform.Find("Expanded/Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(c.name);
            try
            {
                card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText(c.due.ToString());
            }
            catch (NullReferenceException e)
            {
                card.transform.Find("Expanded/Canvas/DueDate").GetComponentInChildren<TextMeshProUGUI>().SetText("No Due Date");
            }
            try
            {
                card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText(c.desc);
            }
            catch (NullReferenceException e)
            {
                card.transform.Find("Expanded/Canvas/Desc").GetComponentInChildren<TextMeshProUGUI>().SetText("No Description");
            }

            if (c.idList == "5d54bbb6da4a043bbc645700")
            {
                card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: ToDo");
            }
            if (c.idList == "5d54bbb8f51b346c3d9303b7")
            {
                card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: Doing");
            }
            if (c.idList == "5d54bbbef54a7c158e8b7f11")
            {
                card.transform.Find("Expanded/Canvas/ListID").GetComponentInChildren<TextMeshProUGUI>().SetText("List: Done");
            }

            if (card.transform.position[1] < 3.75f || card.transform.position[1] > 6.125f)
            {
                card.SetActive(false);
            }
        }
    }

    public void ScrollUp()
    {
        foreach (Transform clone in GameObject.Find("GraphPoints").transform)
        {
            Vector3 p = clone.transform.position;
            p.y += 0.5f;
            clone.transform.position = p;
            DisplayCard(clone);
        }
    }

    public void ScrollDown()
    {
        foreach (Transform clone in GameObject.Find("GraphPoints").transform)
        {
            Vector3 p = clone.transform.position;
            p.y -= 0.5f;
            clone.transform.position = p;
            DisplayCard(clone);
        }
    }

    public void DisplayCard(Transform clone)
    {
        if (clone.transform.position[1] < 3.75f || clone.transform.position[1] > 6.125f)
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
        var parent = GameObject.Find("GraphPoints");

        this.transform.SetParent(parent.transform, true);
    }

}
