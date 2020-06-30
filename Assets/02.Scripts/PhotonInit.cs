using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public InputField userNameInput;
    public InputField roomNameInput;

    private readonly string gameVersion = "v1.0";
    public string userName = "Zackiller";
    public byte maxPlayerCount = 25;
    private bool isConnected = false;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }

    void Start()
    {
        userName = PlayerPrefs.GetString("USER_NAME");
        userNameInput.text = userName;

        if (string.IsNullOrEmpty(userName))
        {
            userName = "Player_" + Random.Range(1, 999).ToString("000");
            userNameInput.text = userName;
        }

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Connected");
        }       
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        } 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.NickName = userName;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"Error Code = {returnCode} Msg = {message}");

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = maxPlayerCount;
        ro.IsOpen     = true;
        ro.IsVisible  = true;

        PhotonNetwork.CreateRoom("MyRoom_" + Random.Range(0, 999).ToString(), ro);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entered room ");
        Debug.Log(PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("BattleField");
        }
    }

    public void OnCreateRoomClick()
    {
        string roomName = "";
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            roomName = "Room_" + Random.Range(1,99).ToString("00");
            roomNameInput.text = roomName;
        }
        
        userName = userNameInput.text;

        PlayerPrefs.SetString("USER_NAME", userName);
        PhotonNetwork.NickName = userName;

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, ro);
    }

    public void OnJoinRandomRoomClick()
    {
        userName = userNameInput.text;

        PlayerPrefs.SetString("USER_NAME", userName);
        PhotonNetwork.NickName = userName;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            string msg = $"{room.Name} [{room.PlayerCount}/{room.MaxPlayers}]";
            Debug.Log(msg);
        }
    }

}
