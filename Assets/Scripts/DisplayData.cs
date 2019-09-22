using UnityEngine;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System;

public class DisplayData : MonoBehaviour
{
    public GameObject data;
    string _WebsiteURL = "https://api.jsonbin.io/b/5d858c62df6c6c5a17b487c2";


    void Start()
    {
        WWW myWww = new WWW(_WebsiteURL);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        DataPoint[] dataItems = JsonReader.Deserialize<DataPoint[]>(jsonResponse);

        for (int i = 0; i < dataItems.Length; i++)
        {
            //var point = (GameObject)Instantiate(data, new Vector3(((float)i / 2) - 6.25f, (dataItems[i].yOffset) - 0.5f, 0), Quaternion.identity);
            var point = (GameObject)Instantiate(data, new Vector3(-18.0f, (dataItems[i].yOffset) + 4.0f, ((float)i / 2) - 6.0f), Quaternion.identity);
            setText(point, dataItems[i]);

        }
    }

    public void setText(GameObject newPoint, DataPoint dataItem)
    {
        newPoint.transform.Find("Data/Expanded/MiddleCanvas/Iteration").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.iteration);
        newPoint.transform.Find("Data/Expanded/LeftCanvas/StartDate").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.startDate);
        newPoint.transform.Find("Data/Expanded/LeftCanvas/EndDate").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.endDate);
        newPoint.transform.Find("Data/Expanded/RightCanvas/Budget").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.budget.ToString());
        newPoint.transform.Find("Data/Expanded/RightCanvas/Phase").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.phase.ToString());
    }

}
