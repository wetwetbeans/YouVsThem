using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Photon.Pun;

public class GunSystem : MonoBehaviour
{
	[Header("Gun Type")]
	public bool isPistol;
	public bool isRifle;
	public bool isShotgun;
	public bool isRocket;
	public bool isSniper;

	[Header("bullet Type")]
	public bool smallBullet;
	public bool LargeBullet;
	public bool Rocket;
	public bool Grenade;

//Gun Stats
	public float damage;
	public float range;
	[SerializeField] float timeBetwwenShooting, spread, reloadTime;
	[SerializeField] int magazineSize, bulletsPerShot, maxAmmo;
	int bulletsLeftInChamber, bulletsLeft;


//Bools
	public bool shooting, readyToShoot, reloading;


//Graphics
	public GameObject muzzleFlash;
//reference
	[SerializeField] GameObject bulletGameObject;
	GameObject UIGunSlot1;
	GameObject UIGunSlot2;
	GameObject gunSlot1;
	GameObject gunSlot2;
	TextMeshProUGUI magazineText;
	TextMeshProUGUI bulletsLeftText;
	PhotonView _photonview;

	public Transform attackPoint;
	
	PlayerControls _playerControls;

    private void Start()
    {
		_photonview = GetComponentInParent<PhotonView>();
		gunSlot1 = GameObject.Find("Gun Slot 1");
		gunSlot2 = GameObject.Find("Gun Slot 2");
		SetAmoText();
		_playerControls = new PlayerControls();
		bulletsLeft = maxAmmo;
		bulletsLeftInChamber = magazineSize;
		readyToShoot = true;
	}

    private void SetAmoText()
    {
        if (gunSlot1 != null && gunSlot1.activeInHierarchy)
        {
			magazineText = GameObject.Find("Magazine Text 1").GetComponent<TextMeshProUGUI>();
			bulletsLeftText = GameObject.Find("Bullets Left Text 1").GetComponent<TextMeshProUGUI>();
		}
		else if (gunSlot2 != null && gunSlot2.activeInHierarchy)
		{
			magazineText = GameObject.Find("Magazine Text 2").GetComponent<TextMeshProUGUI>();
			bulletsLeftText = GameObject.Find("Bullets Left Text 2").GetComponent<TextMeshProUGUI>();
		}
	}



    // On Awake
    private void Awake()
    {
		gunSlot1 = GameObject.Find("Gun Slot 1");
		gunSlot2 = GameObject.Find("Gun Slot 2");
		_playerControls = new PlayerControls();
		bulletsLeft = maxAmmo;
		bulletsLeftInChamber = magazineSize;
		readyToShoot = true;

	}
    protected void OnEnable()
	{
		gunSlot1 = GameObject.Find("Gun Slot 1");
		gunSlot2 = GameObject.Find("Gun Slot 2");
		_playerControls.Enable();
	}


    protected void OnDisable()
	{
		_playerControls.Disable();
	}
	
	
// Do every Frame
	public void FixedUpdate()
	{
		if (PhotonNetwork.InRoom && !_photonview.IsMine)
        {
			return;
        }
		MyInput();

			magazineText.text = bulletsLeftInChamber.ToString();
			bulletsLeftText.text ="/ " + bulletsLeft.ToString();


	}
	public void MyInput ()
	{		
	//check if right thumb stick is at shoot position (set in Movement script)
		shooting = newMovement.shooting;

		//reload (Check if magazibe is full first)
		if (_playerControls.Controls.Reload.IsPressed() &&  bulletsLeftInChamber < magazineSize && !reloading)
		{
			Reload();
		}
		
	//Shoot
		if (readyToShoot && shooting && !reloading && bulletsLeftInChamber > 0)
		{
			Shoot();
		}
	}

    private void Shoot()
    {
		readyToShoot = false;

	// Spread
		float xspread = UnityEngine.Random.Range(-spread, spread);

		//Shoot Projectile
		ShootProjectile();


		bulletsLeftInChamber-= bulletsPerShot;

		Invoke("ResetShot", timeBetwwenShooting);

		if (bulletsLeftInChamber <= 0)
        {
			Reload();
        }
    }

    private void ShootProjectile()
    {
		muzzleFlash.SetActive(true);
		GameObject bullet = Instantiate(bulletGameObject, attackPoint.position, attackPoint.rotation);
		bullet.GetComponent<BulletProjectiles>().player = transform.parent.parent.parent.gameObject;
		Invoke("MuzzleFlashOff", 0.02f);
	}

	private void ResetShot()
    {
		readyToShoot = true;
    }

    // Reload
    void Reload()
	{
		if (bulletsLeftInChamber <= 0 && bulletsLeft <= 0)
			return;
		reloading = true;
		Invoke("ReloadFinished", reloadTime);
	}

	private void ReloadFinished()
    {
		if (bulletsLeft >= magazineSize)
        {
			bulletsLeftInChamber = magazineSize;
			bulletsLeft -= magazineSize;
		}
		else
        {
			bulletsLeftInChamber += bulletsLeft;
			bulletsLeft = 0;
        }

		reloading = false;
    }
	private void MuzzleFlashOff()
    {
		muzzleFlash.SetActive(false);
    }

	public int getMagazineSize()
    {
		return magazineSize;
    }

	public int GetMaxAmmo()
    {
		return maxAmmo;
    }
}

