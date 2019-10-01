using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TrelloConnector : MonoBehaviour
{
    //string key = "e381067d0645392fd30fd1424c01385c";
    //string token = "8e26affbb38740b77f6296dd440ab471625876e15c2de291a74f038781aad59e";
    //string board_id = "5d54b9da290f0936fc738bd6";
    public string key;
    public string token;
    public string board_id;

    void Start()
    {
        string url = $"https://api.trello.com/1/boards/{board_id}/cards?key={key}&token={token}";
        WWW myWww = new WWW(url);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        Card[] cards = JsonConvert.DeserializeObject<Card[]>(jsonResponse);

        for (int i = 0; i < cards.Length; i++)
        {
            Debug.Log(cards[i].name);
        }
    }

}
