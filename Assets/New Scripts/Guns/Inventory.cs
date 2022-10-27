using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	public static GameObject statGunSlot1;
	public static GameObject statGunSlot2;
	public static bool addWeaponSlot1 = false;
	public static bool addWeaponSlot2 = false;

	[Header("Gun 1")]
	[SerializeField] GameObject gun1Slot;
	
	[Header("Gun 2")]
	[SerializeField] GameObject gun2Slot;

// Active Gun Slot Bools
	public bool isGun1;
	public bool isGun2;

//Weapon to add to inventory gameobject
	public GameObject weaponToInstantiate;

// Start
    void Start()
    {
		weaponToInstantiate = null;
		gun1Slot.SetActive(true);
		gun2Slot.SetActive(false);
		isGun1 = true;
		isGun2 = false;
		statGunSlot1 = gun1Slot;
		statGunSlot2 = gun2Slot;
    }


	public void AddWeapon()
    {
		if(gun1Slot.transform.childCount > 0 && gun2Slot.transform.childCount>0)
        {
			Debug.Log("Would You Like To Swap Weapon?");
        }		
		else if (gun1Slot.transform.childCount == 0)
        {
			Instantiate(weaponToInstantiate, gun1Slot.transform);
		// Find UI Gun Slot and change Icon to new weapon	
			transform.parent.gameObject.transform.Find("Canvas - Main In Match UI/Guns/Gun Icons/UI Gun Slot 1").gameObject.GetComponent<UIGunSelection>().SetGunUI();

		}
		else if (gun1Slot.transform.childCount > 0 && gun2Slot.transform.childCount == 0)
        {
			Instantiate(weaponToInstantiate, gun2Slot.transform);
			weaponToInstantiate.GetComponent<GunSystem>().enabled = true;
			// Find UI Gun Slot and change Icon to new weapon	
			transform.parent.gameObject.transform.Find("Canvas - Main In Match UI/Guns/Gun Icons/UI Gun Slot 2").gameObject.GetComponent<UIGunSelection>().SetGunUI();
		}

	}
	public void SwitchWeapon()
    {

		if (isGun1 && gun2Slot.transform.childCount > 0)
		{

			isGun1 = false;
			gun1Slot.SetActive(false);
			isGun2 = true;
			gun2Slot.SetActive(true);
			gun2Slot.GetComponentInChildren<GunSystem>().enabled = true;

		}
		else if (isGun2)
		{


			if (gun1Slot.transform.childCount > 0)
			{
				isGun1 = true;
				gun1Slot.SetActive(true);
				isGun2 = false;
				gun2Slot.SetActive(false);
				gun2Slot.GetComponentInChildren<GunSystem>().enabled = true;

			}
		}

    }

}
