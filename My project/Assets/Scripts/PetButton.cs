using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PetButton : MonoBehaviour
{
    public int itemIdx;
    public string itemID;
    public int cost;
    public TextMeshProUGUI costText;
    public Sprite petImage;
    public GameObject itemButton;
    public GameObject setButton;

    // Start is called before the first frame update
    void Start()
    {
        costText.text = $"{cost}";
        itemButton.GetComponent<Image>().sprite = petImage;
        setButton.GetComponent<Image>().sprite = petImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
