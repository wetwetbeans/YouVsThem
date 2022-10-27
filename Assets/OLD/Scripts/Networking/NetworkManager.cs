using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public GameObject connecting;
    public GameObject Multiplayer;
    LoadScene _loadScene;

    void Start()
    {
        _loadScene = GameObject.FindObjectOfType<LoadScene>();
        Debug.Log("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        connecting.SetActive(false);
        Multiplayer.SetActive(true);
        Debug.Log("Lobby Joined");
    }

    public void FindMatch()
    {
        Debug.Log("Finding Room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        MakeRoom();
    }

    private void MakeRoom()
    {
        int randomRoomName = Random.Range(0, 5000);

        RoomOptions _roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4,
            PublishUserId = true

    };

        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, _roomOptions);
        Debug.Log("Created Room: " + randomRoomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Loading Scene 1");
        PhotonNetwork.LoadLevel("NewScene");
    }
}
