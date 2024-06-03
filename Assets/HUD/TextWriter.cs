using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TMP_Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool isWriting;

    public void AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
        timer = 0;
        isWriting = true;
    }

    private void Update()
    {
        if (isWriting && uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f && characterIndex < textToWrite.Length)
            {
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);
            }
            if (characterIndex >= textToWrite.Length)
            {
                isWriting = false;
                uiText = null;
            }
        }
    }
}


