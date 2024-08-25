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
            string longText = "In an age of iron and fire, the last sovereign, King Aric, "+
                               "stands against an endless tide of monstrous foes. Clad in crimson raiments and an azure cloak," +
                               "he wields a blade blessed by the gods, its edge sharp enough to cleave the essence of evil. " +
                               "The ground beneath his feet is littered with the bones of fallen warriors, "+
                               "the air thick with the stench of decay. " +
                               "From the shadows emerge the relentless legions of goblins, skeletons, and bats, wrought from the darkest corners of the abyss. " +
                            "Will the young king overcome the hordes of darkness, or is this the end of humanity? As the last one standing, " +
                            "King Aric's fate is intertwined with the survival of his kingdom. His struggle is not just for his life, " +
                            "but for the very soul of a world teetering on the brink of annihilation. Stand with him, witness his valor, "+ 
                            "and wonder—can he hold back the night, or will the shadows finally prevail?"+ "\n" +
                            "Press F to continue ...";
            textWriter.AddWriter(messageText, longText, 0.7f);
        }
        else
        {
            Debug.LogError("TextWriter or messageText is not assigned correctly.");
        }
    }
}


