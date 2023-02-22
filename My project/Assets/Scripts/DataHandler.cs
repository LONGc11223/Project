using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Managers;
using TMPro;

public class DataHandler : MonoBehaviour
{
    // public GameObject pet;
    // int lastColor;
    // public int color;
    // public List<Material> petMaterial = new List<Material>();

    // public List<GameObject> pets;
    // public List<GameObject> environments;
    [Header("Shop Fields")]
    public TextMeshProUGUI shopCurrencyText;
    public List<Button> items;


    // Start is called before the first frame update
    void Start()
    {
        // SetPetCosmetics();
        // lastColor = color;
        Debug.Log("Yes");
        if (true)
        {
            Debug.Log("About to try loading data");
            MainManager.Instance.databaseManager.LoadUserData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (color != lastColor)
        // {
        //     SetPetCosmetics();
        // }

        if (MainManager.Instance.databaseManager.currentUserData != null)
        {
            Dictionary<string, object> userData = MainManager.Instance.databaseManager.currentUserData;
            var currency = userData["Currency"];
            shopCurrencyText.text = $"{currency}";
        }
        
    }

    // void SetPetCosmetics()
    // {
    //     // replace the following line with data from the backend
    //     if (color < petMaterial.Count)
    //     {
    //         pet.GetComponent<MeshRenderer>().material = petMaterial[color];
    //         lastColor = color;
    //     }
        
    // }

    // public void CheckData()
    // {
    //     MainManager.Instance.databaseManager.GetData();
    // }
}
