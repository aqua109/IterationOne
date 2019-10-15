using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class NicknameConnector : MonoBehaviour
{
    public string bin_id;
    public Nickname[] nicknames;

    void Awake()
    {
        string url = $"https://api.myjson.com/bins/{bin_id}";
        WWW myWww = new WWW(url);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        nicknames = JsonConvert.DeserializeObject<Nickname[]>(jsonResponse);

        for (int i = 0; i < nicknames.Length; i++)
        {
            Debug.Log(nicknames[i].nickname);
        }
    }
}
