using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public RoomManager Instance;

    private void Awake()
    {

        if (Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(Instance);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 2, Random.Range(-3, 3));
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load("Player"), spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        
    }
}
