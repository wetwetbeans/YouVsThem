using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{


    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void LoadMultiplayer()
    {
        SceneManager.LoadScene("MultiPlayer");
    }

}
