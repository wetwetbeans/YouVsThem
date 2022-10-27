using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGunSelection : MonoBehaviour
{

    [SerializeField] int slotNumber;

    [SerializeField] public bool isSelected = false;

    [SerializeField] GameObject gunSlot;
    [SerializeField] GameObject noWeapon;
    [SerializeField] GameObject selectedBG;

    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI magazineText;
    [SerializeField] TextMeshProUGUI bulletsLeftText;


    // Start is called before the first frame update
    void Start()
    {
       Invoke("SetSlotToGun", 0.5f);       
    }



    // Update is called once per frame
    void Update()
    {
        if (gunSlot != null)
        {
            if (gunSlot.activeSelf)
            {
                isSelected = true;
            }
            else isSelected = false;
        }

        if (isSelected)
            selectedBG.SetActive(true);
        else
            selectedBG.SetActive(false);

    }

    public void SetGunUI()
    {
        if (gunSlot !=null && gunSlot.transform.childCount > 0)
        {
            noWeapon.gameObject.SetActive(false);
            icon.enabled = true;
            selectedBG.SetActive(true);
            icon.sprite = gunSlot.GetComponentInChildren<Image>().sprite;
            icon.SetNativeSize();
            icon.rectTransform.localScale = gunSlot.GetComponentInChildren<Image>().rectTransform.localScale;
            SetInitialAmmoText();
        }
        else
        {
            icon.enabled = false;
            noWeapon.SetActive(true);
        }

    }

    private void SetInitialAmmoText()
    {
        for(int i = 1; i <= 3; i++)
        {
            if (this.name == ("UI Gun Slot " + i.ToString()))
            {
                magazineText = GameObject.Find("Magazine Text " + i.ToString()).GetComponent<TextMeshProUGUI>();
                bulletsLeftText = GameObject.Find("Bullets Left Text " + i.ToString()).GetComponent<TextMeshProUGUI>();
                magazineText.text = GameObject.FindObjectOfType<Inventory>().weaponToInstantiate.GetComponent<GunSystem>().getMagazineSize().ToString();
                bulletsLeftText.text = "/ " + GameObject.FindObjectOfType<Inventory>().weaponToInstantiate.GetComponent<GunSystem>().GetMaxAmmo().ToString();
                return;
            }
        }

    }

    private void SetSlotToGun()
    {

        if (slotNumber == 1)
        {
                gunSlot = Inventory.statGunSlot1;
                SetGunUI();

        }
        else if (slotNumber == 2)
        {
                gunSlot = Inventory.statGunSlot2;
                SetGunUI();
        }
    }

    public void switchWeapon()
    {
        if (isSelected)
            return;
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().SwitchWeapon();
        }
    }

}
