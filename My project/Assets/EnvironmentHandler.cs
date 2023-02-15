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
        GreatDane
    }
    public ActivePet activePet;
    ActivePet currentPet;

    public List<GameObject> pets;

    public enum ActiveEnvironment
    {
        Original,
        Bamboo,
        Forest
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
            }
            currentEnvironment = activeEnvironment;
        }
    }
}
