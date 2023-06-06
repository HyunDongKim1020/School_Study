using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenMange : MonoBehaviour
{
    public GameObject GuideBTN;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
       
         SceneManager.LoadScene("Game");
        
    }

    public void Guide()
    {
       
        if (GuideBTN.activeSelf)
        {
            GuideBTN.SetActive(false);            
        }
        else
        {
            GuideBTN.SetActive(true);            
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Á¾·á");
    }
}
