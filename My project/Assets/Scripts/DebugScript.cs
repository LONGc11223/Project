using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;

public class DebugScript : MonoBehaviour
{
    public TMP_InputField calorieField;

    public void Reset()
    {
        Debug.Log("Resetting exercise stats!");
        calorieField.text = "0";

        Debug.Log("Adding an exercise minute!");
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
                { "HealthData.ExerciseMinutes", 0 },
                { "HealthData.CaloriesBurned", 0 },
        };


        MainManager.Instance.databaseManager.UpdateData(updates);
    }

    public void AddMinutes()
    {
        Debug.Log("Adding an exercise minute!");
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
                { "HealthData.ExerciseMinutes", 30 }
        };


        MainManager.Instance.databaseManager.UpdateData(updates);
    }

    public void AddCalories()
    {
        Debug.Log($"Adding {calorieField.text} calories!");

        // Write out the data
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
                { "HealthData.CaloriesBurned", 500 }
        };


        MainManager.Instance.databaseManager.UpdateData(updates);
    }
}
