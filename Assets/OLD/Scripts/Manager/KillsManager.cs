using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillsManager : MonoBehaviour
{
    public int kills = 0;
    TextMeshProUGUI killsText;

    // Start is called before the first frame update
    void Start()
    {
        killsText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        killsText.text = kills.ToString();
    }

    public int addKill(int amountToAdd)
    {
        kills +=amountToAdd;
        return kills;
    }
    
}
