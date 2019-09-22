using UnityEngine;
using UnityEngine.UI;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;

public class Tablet : MonoBehaviour
{
    public GameObject user;
    public List<GameObject> users = new List<GameObject>();

    public float x_origin;
    public float y_origin;
    public float z_origin;

    string _WebsiteURL = "https://api.jsonbin.io/b/5d6f08818ea2fe6d64ebdf89";
    public TabletData[] dataItems;
    int listLength;
    GameObject lastUser;
    float x = -4.0f;
    float y = 1.0f;
    float z = 3.0f;
    float offset = 3.5f;
    private int i = 0;

    void Awake()
    {
        WWW myWww = new WWW(_WebsiteURL);
        while (myWww.isDone == false) ;
        string jsonResponse = myWww.text;

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        dataItems = JsonReader.Deserialize<TabletData[]>(jsonResponse);

        //foreach (TabletData dataItem in dataItems)
        //{
        //    var newUser = (GameObject)Instantiate(user, new Vector3(x, y, z), Quaternion.identity);
        //    StartCoroutine(setAvatar(dataItem.avatar, newUser));
        //    setText(newUser, dataItem);
        //    x = x + offset;
        //    if (x > 10)
        //    {
        //        x = -4.0f;
        //        y = y + 2.0f;
        //    }

        //    setSliders(newUser, dataItem);
        //}
    }

    public void setSliders(GameObject newUser, TabletData dataItem)
    {
        float ux = dataItem.user_experience * (float)0.12;
        float cd = dataItem.coding * (float)0.12;
        float dd = dataItem.data_design * (float)0.12;
        float pm = dataItem.project_management * (float)0.12;
        float az = dataItem.azure_services * (float)0.12;
        if (dataItem.user_experience == 1)
        {
            ux = 0.0f;
        }
        if (dataItem.coding == 1)
        {
            cd = 0.0f;
        }
        if (dataItem.data_design == 1)
        {
            dd = 0.0f;
        }
        if (dataItem.project_management == 1)
        {
            pm = 0.0f;
        }
        if (dataItem.azure_services == 1)
        {
            az = 0.0f;
        }
        newUser.transform.Find("user_experience").transform.Translate(ux, 0, 0);
        newUser.transform.Find("coding").transform.Translate(cd, 0, 0);
        newUser.transform.Find("data_design").transform.Translate(dd, 0, 0);
        newUser.transform.Find("project_management").transform.Translate(pm, 0, 0);
        newUser.transform.Find("azure_services").transform.Translate(az, 0, 0);
    }

    private IEnumerator setAvatar(string url, GameObject newUser)
    {
        WWW www = new WWW(url);
        yield return www;

        newUser.transform.Find("Canvas/avatar").GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        www.Dispose();
        www = null;
    }

    public void setText(GameObject newUser, TabletData dataItem)
    {
        newUser.transform.Find("Canvas/first_name").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.first_name);
        newUser.transform.Find("Canvas/last_name").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.last_name);
        newUser.transform.Find("Canvas/group_number").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.group_number.ToString());
        newUser.transform.Find("Canvas/email").GetComponentInChildren<TextMeshProUGUI>().SetText(dataItem.email);
    }

    public void createuser()
    {
        var newUser = (GameObject)Instantiate(user, new Vector3(x_origin, y_origin, z_origin), Quaternion.identity);
        users.Add(newUser);
        setText(newUser, dataItems[i % 49]);
        StartCoroutine(setAvatar(dataItems[i % 49].avatar, newUser));
        setSliders(newUser, dataItems[i % 49]);
        i++;
    }

    public void removeUser()
    {
        listLength = users.Count;
        lastUser = users[listLength - 1];
        users.Remove(lastUser);
        Destroy(lastUser);
        i--;
    }

}
