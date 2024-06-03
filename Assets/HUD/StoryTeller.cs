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
            string longText = "Once upon a time, in a faraway land, there was a tall tower. " +
                              "This tower was home to many adventurers who sought to conquer its heights and uncover its secrets. " +
                              "Among them was a brave warrior named Arin, who dreamed of reaching the top and finding the legendary treasure said to be hidden there. " +
                              "With each step, the challenges grew harder, but Arin's resolve never wavered. " +
                              "Through battles and puzzles, friendships and betrayals, the journey to the top of the tower was an epic tale of courage and determination. " +
                              "As Arin climbed higher, the air grew thin and the view more breathtaking. The legend of the tower had captured the hearts of many, " +
                              "but only the truly brave would ever discover its true nature.";
            textWriter.AddWriter(messageText, longText, 0.05f);
        }
        else
        {
            Debug.LogError("TextWriter or messageText is not assigned correctly.");
        }
    }
}


