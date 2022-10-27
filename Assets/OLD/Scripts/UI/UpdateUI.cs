using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{

    GameObject gunSlot2;

    private void OnEnable()
    {
        gunSlot2 = GameObject.Find("UI Gun Slot 2");
        gunSlot2.GetComponent<UIGunSelection>().SetGunUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
