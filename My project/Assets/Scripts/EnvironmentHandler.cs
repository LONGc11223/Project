using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentHandler : MonoBehaviour
{
    public Light directionalLight;
    public Camera mainCamera;
    int currentPetIdx;
    int currentEnvIdx;

    public enum ActivePet
    {
        GoldenRetriever,
        Chihuahua,
        GreatDane,
        Tiger,
        Panda,
        GreyWolf,
    }
    public ActivePet activePet;
    ActivePet currentPet;

    public List<GameObject> pets;

    public enum ActiveEnvironment
    {
        Original,
        Bamboo,
        Forest,
        Field,
    }
    public ActiveEnvironment activeEnvironment;
    ActiveEnvironment currentEnvironment;

    public List<GameObject> environments;

    // Update is called once per frame
    void Update()
    {
        if (currentPet != activePet)
        {
            switch (activePet)
            {
                case ActivePet.GoldenRetriever:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 0;
                    pets[currentPetIdx].SetActive(true);
                    break;
                case ActivePet.Chihuahua:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 1;
                    pets[currentPetIdx].SetActive(true);
                    break;
                case ActivePet.GreatDane:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 2;
                    pets[currentPetIdx].SetActive(true);
                    break;
                case ActivePet.Tiger:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 3;
                    pets[currentPetIdx].SetActive(true);
                    break;
                case ActivePet.Panda:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 4;
                    pets[currentPetIdx].SetActive(true);
                    break;
                case ActivePet.GreyWolf:
                    pets[currentPetIdx].SetActive(false);
                    currentPetIdx = 5;
                    pets[currentPetIdx].SetActive(true);
                    break;
            }
            currentPet = activePet;
        }

        if (currentEnvironment != activeEnvironment)
        {
            switch (activeEnvironment)
            {
                case ActiveEnvironment.Original:
                    environments[currentEnvIdx].SetActive(false);
                    mainCamera.backgroundColor = new Color(1f, 0.980f, 0.831f, 1f);
                    directionalLight.color = new Color(1f, 0.561f, 0.322f, 1f);
                    directionalLight.transform.localEulerAngles = new Vector3(50f,-30f,0f);
                    currentEnvIdx = 0;
                    environments[currentEnvIdx].SetActive(true);
                    break;
                case ActiveEnvironment.Bamboo:
                    environments[currentEnvIdx].SetActive(false);
                    mainCamera.backgroundColor = new Color(0.776f, 0.831f, 0.616f, 1f);
                    directionalLight.color = new Color(1f, 0.82f, 0.588f, 1f);
                    directionalLight.transform.localEulerAngles = new Vector3(45f,-435f,0f);
                    currentEnvIdx = 1;
                    environments[currentEnvIdx].SetActive(true);
                    break;
                case ActiveEnvironment.Forest:
                    environments[currentEnvIdx].SetActive(false);
                    mainCamera.backgroundColor = new Color(0.576f, 0.655f, 0.702f, 1f);
                    directionalLight.color = new Color(1f, 0.82f, 0.588f, 1f);
                    directionalLight.transform.localEulerAngles = new Vector3(45f,-435f,0f);
                    currentEnvIdx = 2;
                    environments[currentEnvIdx].SetActive(true);
                    break;
                case ActiveEnvironment.Field:
                    environments[currentEnvIdx].SetActive(false);
                    mainCamera.backgroundColor = new Color(0.6157f, 0.811f, 0.854f, 1f);
                    directionalLight.color = new Color(0.8f, 0.85f, 0.898f, 1f);
                    directionalLight.transform.localEulerAngles = new Vector3(60f,-400f,66f);
                    currentEnvIdx = 3;
                    environments[currentEnvIdx].SetActive(true);
                    break;
            }
            currentEnvironment = activeEnvironment;
        }
    }

    public void SetPet(int idx)
    {
        switch(idx)
        {
            case 0:
                activePet = ActivePet.GoldenRetriever;
                break;
            case 1:
                activePet = ActivePet.Chihuahua;
                break;
            case 2:
                activePet = ActivePet.GreatDane;
                break;
        }
    }

    public void SetEnv(int idx)
    {
        switch(idx)
        {
            case 0:
                activeEnvironment = ActiveEnvironment.Original;
                break;
            case 1:
                activeEnvironment = ActiveEnvironment.Bamboo;
                break;
            case 2:
                activeEnvironment = ActiveEnvironment.Forest;
                break;
        }
    }
}
