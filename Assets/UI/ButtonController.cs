using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{   
    public static ButtonController instance;
    public bool canMoveText = true;
    [SerializeField] private Button[] button;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SetButtonInteractable(bool value)
    {
        foreach (Button b in button)
        {
            b.interactable = value;
            canMoveText = false;

        }
        
    }
}
