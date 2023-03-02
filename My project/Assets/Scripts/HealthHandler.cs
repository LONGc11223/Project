using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Managers;
using TMPro;

public class HealthHandler : MonoBehaviour
{
    public bool debug;
    public Image moveRing;
    public Image exerciseRing;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI debugText;

    double moveRingValue;
    double moveRingGoal;
    double exerciseRingValue;
    bool moveRewardToday;
    bool exerciseRewardToday;
    
    // Start is called before the first frame update
    void Start()
    {
        moveRingValue = 450;
        moveRingGoal = 500;
        exerciseRingValue = 15;
        if (!debug)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!debug)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
        }
        if (moveRingGoal < 250)
        {
            moveRingGoal = 250;
        }
        progressText.text = $"{Convert.ToInt32(moveRingValue / (double)moveRingGoal)}%";
        moveRing.fillAmount = (float)(moveRingValue / moveRingGoal);
        exerciseRing.fillAmount = (float)(exerciseRingValue / 30);

        debugText.text = $"Move - {moveRing}\nExercise - {exerciseRing}\nGoal - {moveRingGoal}";

        if (!exerciseRewardToday && exerciseRingValue >= 30.0)
        {
            exerciseRewardToday = true;
            MainManager.Instance.databaseManager.AddFunds(100);
        }

        if (!moveRewardToday && (moveRingValue / moveRingGoal) >= 1.0)
        {
            moveRewardToday = true;
            MainManager.Instance.databaseManager.AddFunds(50);
        }
    }
}
