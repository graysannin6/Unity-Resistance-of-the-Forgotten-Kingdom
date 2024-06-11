using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTaller : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private TMP_Text messageText;

    private void Start()
    {
        if (textWriter != null && messageText != null)
        {
            string longText = "In the kingdoms forgotten by time," +
                              " a final battle for survival is waged." +
                              " Endless waves of the vilest creatures emerge from the heart of the earth to devour and destroy all in their path." +
                              " After a tragic battle, a young king is the sole bulwark between his kingdom and annihilation." +
                              " Shall he find grace in the heavens, or will this be his last crusade?" + "\n" + "Press F to continue.";
            textWriter.AddWriter(messageText, longText, 0.07f);
        }
        else
        {
            Debug.LogError("TextWriter or messageText is not assigned correctly.");
        }
    }
}


