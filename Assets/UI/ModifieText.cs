using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ModifieText : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI textx;
    [SerializeField] private float yoffset = 5.0f;
    [SerializeField] private float alpha = 0.2f;
    private Vector2 originalPosition;
    private string pressedColorHex = "#F1EEE9";
    private Color pressedColor;
    private Color originalColor;
    

    // Start is called before the first frame updatse
    private void Start()
    {   
        if(textx != null)
        {
            RectTransform rectTransform = textx.GetComponent<RectTransform>();
            originalPosition = rectTransform.anchoredPosition;
            originalColor = textx.color;
            if(ColorUtility.TryParseHtmlString(pressedColorHex, out pressedColor))
            {
                pressedColor.a = alpha;
            }
            else
            {
                Debug.Log("Color not parsed");
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {   
        if(ButtonController.instance.canMoveText)
        {
            RectTransform rectTransform = textx.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y - yoffset);
            textx.color = pressedColor;
        }
        
        
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        RectTransform rectTransform = textx.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = originalPosition;
        Color colorFullAlpha = originalColor;
        colorFullAlpha.a = 1.0f;
        textx.color = colorFullAlpha;
        
    }
}

