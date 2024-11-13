using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";
    private string userId = "Undoe";

    public TMP_InputField userIf;
    public TMP_InputField roomNameIf;

    [Header("Room List")]
    private Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>();
    private GameObject roomItemPrefab;
    public Transform scrollContent;


    void Awake()
    {
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.AutomaticallySyncScene = true;

        roomItemPrefab = Resources.Load<GameObject>("RoomItem");

        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void Start()
    {
        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(1, 21):00}");
        userIf.text = userId;
        PhotonNetwork.NickName = userId;
    }

    public void SetUserId()
    {
        if (string.IsNullOrEmpty(userIf.text))
        {
            userId = $"USER_{Random.Range(1, 21):00}";
        }
        else
        {
            userId = userIf.text;
        }

        PlayerPrefs.SetString("USER_ID", userId);
        PhotonNetwork.NickName = userId;
    }

    string SetRoomName()
    {
        if (string.IsNullOrEmpty(roomNameIf.text))
        {
            roomNameIf.text = $"ROOM_{Random.Range(1, 101):000}";
        }

        return roomNameIf.text;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filed {returnCode}:{message}");

        OnMakeRoomClick();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("PhotonGameSample");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;

        foreach (var roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList == true)
            {
                rooms.TryGetValue(roomInfo.Name, out tempRoom);

                Destroy(tempRoom);

                rooms.Remove(roomInfo.Name);
            }

            else
            {
                if (rooms.ContainsKey(roomInfo.Name) == false)
                {
                    GameObject roomPrefab = Instantiate(roomItemPrefab, scrollContent);
                    roomPrefab.GetComponent<RoomData>().RoomInfo = roomInfo;

                    rooms.Add(roomInfo.Name, roomPrefab);
                }
                else
                {
                    rooms.TryGetValue(roomInfo.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = roomInfo;
                }
            }



            Debug.Log($"{roomInfo.Name} {roomInfo.PlayerCount}/{roomInfo.MaxPlayers}");


        }
    }

    public void OnLoginClick()
    {
        SetUserId();

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnMakeRoomClick()
    {
        SetUserId();

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom(SetRoomName(), ro);
    }

}