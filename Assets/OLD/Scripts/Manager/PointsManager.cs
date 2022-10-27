using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    TextMeshProUGUI score;
    PlayerManager _playerManager;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponentInChildren<TextMeshProUGUI>();
        _playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerManager.CurrentScore() > 0)
        score.text = _playerManager.CurrentScore().ToString();
    }
}
