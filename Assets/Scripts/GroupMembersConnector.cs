using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GroupMembersConnector : MonoBehaviour
{
    public TabletData[] tablets;

    void Awake()
    {
        string url = "https://api.jsonbin.io/b/5da64aa05d7043458ee9334a";
        WWW myWww = new WWW(url);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        tablets = JsonConvert.DeserializeObject<TabletData[]>(jsonResponse);
    }
}