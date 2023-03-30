using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
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
    // public List<Button> items;
    public List<PetButton> petItems;
    public List<EnvironmentButton> environmentItems;
    public bool debug;


    // Start is called before the first frame update
    void Start()
    {
        // SetPetCosmetics();
        // lastColor = color;
        Debug.Log("Yes");
        if (!debug)
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
        // Debug.Log(MainManager.Instance.databaseManager.currentUserData);
        
        if (!debug && MainManager.Instance.databaseManager.currentUserData != null)
        {
            Dictionary<string, object> userData = MainManager.Instance.databaseManager.currentUserData;

            var currency = userData["Currency"];
            shopCurrencyText.text = $"{currency}";
            
            foreach (PetButton item in petItems)
            {
                if (MainManager.Instance.databaseManager.unlockedPets.Contains(item.itemID))
                {
                    //Debug.Log($"User has this unlocked: {item.itemID}");
                    item.setButton.SetActive(true);
                }
            }

            foreach (EnvironmentButton item in environmentItems)
            {
                if (MainManager.Instance.databaseManager.unlockedEnvironments.Contains(item.itemID))
                {
                    //Debug.Log($"User has this unlocked: {item.itemID}");
                    item.setButton.SetActive(true);
                }
            }
        }
        
    }

    public void PurchaseButton(GameObject button)
    {
        if (button.GetComponent<PetButton>() != null)
        {
            Debug.Log("Attempting to buy a pet!");
            PetButton item = button.GetComponent<PetButton>();
            string id = item.itemID;
            int cost = item.cost;
            MainManager.Instance.databaseManager.PurchaseItem(id, cost, 0);
        }
        else if (button.GetComponent<EnvironmentButton>() != null)
        {
            Debug.Log("Attempting to buy an environment!");
            EnvironmentButton item = button.GetComponent<EnvironmentButton>();
            string id = item.itemID;
            int cost = item.cost;
            MainManager.Instance.databaseManager.PurchaseItem(id, cost, 1);
        }
        // MainManager.Instance.databaseManager.PurchaseItem(idx, cost);
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
