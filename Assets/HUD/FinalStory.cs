using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalStory : MonoBehaviour
{
   [SerializeField] private TextWriter textWriter;
    [SerializeField] private TMP_Text messageText;

    private void Start()
    {
        if (textWriter != null && messageText != null)
        {
            string longText = "In a climactic battle that raged for days, King Aric fought with unmatched fury. " + 
                              "His god-blessed blade glowing with divine light as it cleaved through endless waves of goblins, skeletons, and bats. " + 
                              "As the final demon fell, silence descended upon the battlefield. Aric, the sole survivor of humanity, stood alone amidst the carnage, " + 
                              "burdened by the weight of his victory. In this moment of solitude, a resplendent light broke through the darkened skies, and the gods, moved by his bravery, " +
                              "enveloped him in divine radiance. As he ascended, a voice echoed, honoring his courage and inviting him to join the celestial ranks. " +
                              "King Aric, the last king of a fallen world,  was welcomed into the celestial realm, his legacy of valor and determination enduring through the ages. " + "\n" +
                              "Press F to continue ...";
            textWriter.AddWriter(messageText, longText, 0.07f);
        }
        else
        {
            Debug.LogError("TextWriter or messageText is not assigned correctly.");
        }
    }
}
