using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BulletProjectiles : MonoBehaviour
{
  //Bullet Type Game Objects
    [SerializeField] GameObject smallBullet;
    [SerializeField] GameObject largeBullet;
    [SerializeField] GameObject Rocket;
    [SerializeField] GameObject Grenade;
              public GameObject player;


  //Bullet Force
    public float shootForce, upwardForce;

  //damage
    [SerializeField] float damage;

  //bool
    bool isFiring = true;

    //reference
    GunSystem _gunSystem;
    Rigidbody _rigidbody;

    private void Start()
    {
        _gunSystem = GameObject.FindWithTag("Player Weapons").GetComponentInChildren<GunSystem>();
        _rigidbody = GetComponent<Rigidbody>();
        SetBulletGameObject();

        damage = _gunSystem.damage;

        if(isFiring)
        Fire();

        Invoke("DestroyGameObject", 3);
    }

    private void SetBulletGameObject()
    {
        if (_gunSystem.smallBullet)
            smallBullet.SetActive(true);
        else if (_gunSystem.LargeBullet)
            largeBullet.SetActive(true);
        else if (_gunSystem.Rocket)
            Rocket.SetActive(true);
        else if (_gunSystem.Grenade)
            Grenade.SetActive(true);
    }

    private void Fire()
    {
        isFiring = false;

        _rigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }

    void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }


    public float GetDamage()
    {
        return damage;
    }
}
