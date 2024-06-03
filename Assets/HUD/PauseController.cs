using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _pauseMenu;
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            PauseGame();
        }
        
    }

    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
    }
}
