using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class restart : MonoBehaviour
{
	PlayerManager _playerManager;
    PlayerControls _playerControls;

    private void Start()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {

       // _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    void Update()
	{
		/*if (_playerManager.CheckHealth() == 0 || transform.position.y <= -15)// || _playerControls.Controls.Reload.triggered)
        {
			Invoke("LoadScene", 5);
        }*/
    }

	void LoadScene()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LoadLevel("MultiPlayer");
        }
		SceneManager.LoadScene("MultiPlayer");
	}
}
