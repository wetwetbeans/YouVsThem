using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDTextObjects : MonoBehaviour
{
    [SerializeField] GameObject gunSlot1;
    [SerializeField] GameObject gunSlot2;
    [SerializeField] public static GameObject reloadingText;
    [SerializeField] public static TextMeshProUGUI magazineText;
    [SerializeField]  TextMeshProUGUI mag;
    [SerializeField] public static TextMeshProUGUI bulletsLeftText;
    [SerializeField] TextMeshProUGUI bull;

    private void Start()
    {
        if (GameObject.Find("Gun Slot 1") != null)
        {
            gunSlot1 = GameObject.Find("Gun Slot 1");
        }

        if (GameObject.Find("Gun Slot 2") != null)
        {
            gunSlot2 = GameObject.Find("Gun Slot 2");
        }

        reloadingText = GameObject.Find("Reloading");
       // reloadingText.SetActive(false);
       // uiSlot1 = GameObject.Find("UI Gun Slot 1");
       // uiSlot2 = GameObject.Find("UI Gun Slot 1");

    }

    private void Update()
    {
        if (gunSlot1 == null)
        {
            if (GameObject.Find("Gun Slot 1") != null)
            {
                gunSlot1 = GameObject.Find("Gun Slot 1");
            }
        }
        if (gunSlot2 == null)
        {
            if (GameObject.Find("Gun Slot 2") != null)
            {
                gunSlot1 = GameObject.Find("Gun Slot 2");
            }
        }

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

}
