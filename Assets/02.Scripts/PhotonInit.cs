using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
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
    }
}
