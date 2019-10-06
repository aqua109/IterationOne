using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TrelloConnector : MonoBehaviour
{
    public string key;
    public string token;
    public string board_id;
    public Card[] cards;

    void Awake()
    {
        string url = $"https://api.trello.com/1/boards/{board_id}/cards?key={key}&token={token}";
        WWW myWww = new WWW(url);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        cards = JsonConvert.DeserializeObject<Card[]>(jsonResponse);

        //for (int i = 0; i < cards.Length; i++)
        //{
        //    Debug.Log(cards[i].name);
        //}
    }
}
