using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;


// use IPunObservable to explicitly observe and synch beam firing
public class VRPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Public Fields
    [Tooltip("Local Player Instance. Use this to know if the local player is represented in the Scene.")]
    public static GameObject LocalPlayerInstance;
    public GameObject Head;
    public TextMeshProUGUI PlayerName;
    public GameObject TextPositioner;
    public GameObject playerCameraToFollow;
    private object[] instantiationData;
    //private static string nickname = "default script name";
    GameObject nickname;
    public static PhotonView pView;

    #endregion


    #region Private Methods
#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif


    #endregion
    #region MonoBehaviour Callbacks
    void Awake()
    {
        instantiationData = gameObject.GetComponent<PhotonView>().InstantiationData;
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject; //the prefab model this is attached to, redundant since you can just use gameObje
        }

        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {

        //nickname = PhotonNetwork.Instantiate("Nickname", new Vector3(0, 6, 0), Quaternion.identity, 0);
        pView = this.GetComponent<PhotonView>();
        pView.RPC("UpdateNicknameText", RpcTarget.AllBuffered, pView.ViewID, "Default Nickname");
        Debug.Log(pView.ViewID);

        if (photonView.IsMine)
        {
            Head.GetComponent<MeshRenderer>().enabled = false;
            PlayerName.GetComponent<TextMeshProUGUI>().enabled = false;
        }

#if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }

    [PunRPC]
    void UpdateNicknameText(int id, string nick)
    {
        var name = PhotonView.Find(id).gameObject;
        name.transform.Find("Headset/Canvas/Title").GetComponentInChildren<TextMeshProUGUI>().SetText(nick);
    }

        void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }


    void OnTriggerStay(Collider other)
    {
    }

    public void Nickname(string nickname, int id)
    {
        pView.RPC("UpdateNicknameText", RpcTarget.AllBuffered, id, nickname);

    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            //TextPositioner.transform.LookAt(playerCameraToFollow.transform);
            return;
        }
        if (LocalPlayerInstance == null)
        {
            Debug.Log("LocalPlayerInstance does not exist");
            return;
        }
        if (playerCameraToFollow == null)
        {
            Debug.Log("PlayerCameraToFollow does not exist");
            return;
        }
            
            
        Head.transform.position = playerCameraToFollow.transform.position;
        Head.transform.rotation = playerCameraToFollow.transform.rotation;

        //TextPositioner.transform.position = playerCameraToFollow.transform.position;
        //TextPositioner.transform.position += TextPositioner.transform.position + new Vector3(0, .5f, 0);
        //TextPositioner.transform.rotation = playerCameraToFollow.transform.rotation;

        TextPositioner.transform.position = Head.transform.position + new Vector3(0, .5f, 0);
        TextPositioner.transform.rotation = playerCameraToFollow.transform.rotation;

        //nickname.transform.position = playerCameraToFollow.transform.position;
        //nickname.transform.rotation = playerCameraToFollow.transform.rotation;


    }

#if !UNITY_5_4_OR_NEWER
    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }
#endif

    void CalledOnLevelWasLoaded(int level)
    {
                       
    }

#if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif

    #endregion

    #region Custom



#endregion

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        if (stream.IsWriting)
        {
            // we own this player, send the others our data
            stream.SendNext(IsFiring);
            stream.SendNext(Health);
        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
        */
    }
    #endregion


}


