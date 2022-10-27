using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Slider _slider;
    PlayerManager _playerManager;
    // Start is called before the first frame update
    void Start()
    {
        _playerManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerManager>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = _playerManager.CheckHealth();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _playerManager.CheckHealth();
    }
}
