using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject weapon;
    public float price;
    [SerializeField] float rotateSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Inventory _inventory;
            _inventory = other.gameObject.GetComponentInChildren<Inventory>();
            _inventory.weaponToInstantiate = weapon;
            _inventory.AddWeapon();
            Destroy(this.gameObject);


        }
    }
}
