using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] int health = 3;
    [SerializeField] Animator _anim;
    public bool isPlayerDead = false;
    Movement _twinStickMovement;
    [SerializeField] GameObject weapons;
    [SerializeField] int score;
    [SerializeField] GameObject ui;
    [SerializeField] int kills = 0;

    PhotonView _photonView;

    public void Hit(int damage)

    {
        health -= damage;
        Debug.Log(health);
    }


    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.InRoom && !_photonView.IsMine)
        {
            ui.SetActive(false);
            return;
        }
        _anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (!_photonView.IsMine)
            return;
        if (health == 0)
        {
            Dying();
        }
    }

    private void Dying()
    {
        _twinStickMovement = GetComponent<Movement>();
     // Remove Weapon & Disable Movement & set dying animation
        weapons.SetActive(false);
        _twinStickMovement.enabled = false;
        isPlayerDead = true;
        _anim.SetBool("Dying", true);
    }

    public int CheckHealth()
    {
        return health;
    }

    public int CurrentScore()
    {
        return score;
    }
    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void AddToKills(int amountOfKillsToAdd)
    {
        kills += amountOfKillsToAdd;
    //Update UI
        transform.Find("Canvas - Main In Match UI/Player Info Panel/Kills UI").gameObject.GetComponent<KillsManager>().addKill(amountOfKillsToAdd);
    }
}
