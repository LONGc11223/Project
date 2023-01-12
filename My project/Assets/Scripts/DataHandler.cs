using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class DataHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckData()
    {
        MainManager.Instance.databaseManager.GetData();
    }
}
