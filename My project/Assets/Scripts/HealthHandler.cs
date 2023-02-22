using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Managers;
using TMPro;

public class HealthHandler : MonoBehaviour
{
    public Image moveRing;
    public Image exerciseRing;
    public TextMeshProUGUI progressText;

    double moveRingValue;
    double moveRingGoal;
    double exerciseRingValue;
    
    // Start is called before the first frame update
    void Start()
    {
        moveRingValue = 450;
        moveRingGoal = 500;
        exerciseRingValue = 15;
        if (MainManager.Instance != null)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.Instance != null)
        {
            moveRingValue = MainManager.Instance.healthManager.GetMoveRing();
            moveRingGoal = MainManager.Instance.healthManager.GetMoveGoal();
            exerciseRingValue = MainManager.Instance.healthManager.GetExerciseRing();
        }
        progressText.text = $"{(int)(moveRingValue / moveRingGoal)}%";
        moveRing.fillAmount = (float)(moveRingValue / moveRingGoal);
        exerciseRing.fillAmount = (float)(exerciseRingValue / 30);
    }
}
