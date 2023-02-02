using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoScreenHandler : MonoBehaviour
{
    public int currentPage;
    public TextMeshProUGUI textDisplay;
    public Image smallImage;
    public Image largeImage;
    public GameObject pageMarker;
    
    public List<Sprite> imageContents = new List<Sprite>();
    [TextArea(5, 7)]
    public List<string> textContents = new List<string>();

    void Start()
    {
        UpdatePage();
    }

    void UpdatePage()
    {
        textDisplay.text = textContents[currentPage];
        if (textDisplay.text == "")
        {
            largeImage.gameObject.SetActive(true);
            largeImage.sprite = imageContents[currentPage];
        }
        else
        {
            largeImage.gameObject.SetActive(false);
            smallImage.sprite = imageContents[currentPage];
        }

        switch(currentPage)
        {
            case 0:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                break;
            case 1:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                pageMarker.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                pageMarker.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void NextPage()
    {
        if (currentPage < textContents.Count - 1) 
        {
            currentPage++;
            UpdatePage();
        }
        
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
        
    }
}
