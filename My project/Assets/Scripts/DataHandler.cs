using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class DataHandler : MonoBehaviour
{
    public GameObject pet;
    int lastColor;
    public int color;
    public List<Material> petMaterial = new List<Material>();


    // Start is called before the first frame update
    void Start()
    {
        SetPetCosmetics();
        lastColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (color != lastColor)
        {
            SetPetCosmetics();
        }
    }

    void SetPetCosmetics()
    {
        // replace the following line with data from the backend
        if (color < petMaterial.Count)
        {
            pet.GetComponent<MeshRenderer>().material = petMaterial[color];
            lastColor = color;
        }
        
    }

    public void CheckData()
    {
        MainManager.Instance.databaseManager.GetData();
    }
}
