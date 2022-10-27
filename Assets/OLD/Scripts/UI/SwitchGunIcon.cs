using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGunIcon : MonoBehaviour
{
    [SerializeField] GameObject pistolIcon;
    [SerializeField] GameObject rifleIcon;
    [SerializeField] GameObject shotGunIcon;
    [SerializeField] GameObject rocketIcon;
    [SerializeField] GameObject sniperIcon;
    public void ChangeGunIcon()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject _weapons = GameObject.Find("Weapons");
        if (_weapons.transform.GetComponentInChildren<GunSystem>().isPistol)
            pistolIcon.SetActive(true);
        else if (_weapons.transform.GetComponentInChildren<GunSystem>().isRifle)
            rifleIcon.SetActive(true);
        else if (_weapons.transform.GetComponentInChildren<GunSystem>().isShotgun)
            shotGunIcon.SetActive(true);
        else if (_weapons.transform.GetComponentInChildren<GunSystem>().isRocket)
            rocketIcon.SetActive(true);
        else if (_weapons.transform.GetComponentInChildren<GunSystem>().isSniper)
            sniperIcon.SetActive(true);


    }
}
