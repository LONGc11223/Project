using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPageScript : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textDisplay;
    [TextArea(5, 7)]
    public string description;

    [Header("Image")]
    public Image smallImage;
    public Image largeImage;
    public Sprite imageContent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.text = description;

        if (description == "")
        {
            largeImage.gameObject.SetActive(true);
            largeImage.sprite = imageContent;
        }
        else
        {
            largeImage.gameObject.SetActive(false);
            smallImage.sprite = imageContent;
        }
    }
}
