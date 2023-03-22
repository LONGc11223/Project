using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class HealthHandlerDebug : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void _requestAuthorization();

    [DllImport("__Internal")]
    private static extern double _getCurrentMoveRingValue();

    [DllImport("__Internal")]
    private static extern double _getCurrentExerciseRingValue();

    [DllImport("__Internal")]
    private static extern double _getMoveRingGoal();

    [DllImport("__Internal")]
    private static extern double _getMoveRingValue(int day, int month, int year);
    public bool debug;
    public double moveRing;
    public double exerciseRing;
    public double moveGoal;

    public double targetMove;

    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;

    // public IEnumerator GetRingData(int day, int month, int year, MoveRingCallbackDelegate moveCallback, ExerciseRingCallbackDelegate exerciseCallback)
    // {
    //     bool moveDone = false;
    //     bool exerciseDone = false;

    //     _getMoveRingForDay(day, month, year,  (double value) => {
    //         moveDone = true;
    //         moveCallback(value);
    //     });

    //     _getExerciseRingForDay(day, month, year, (double value) => {
    //         exerciseDone = true;
    //         exerciseCallback(value);
    //     });

    //     while (!moveDone || !exerciseDone)
    //     {
    //         yield return null;
    //     }
    // }

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        if (!debug) 
        {
            _requestAuthorization();
            moveRing = _getCurrentMoveRingValue();
            exerciseRing = _getCurrentExerciseRingValue();
            moveGoal = _getMoveRingGoal();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int day = int.Parse(dayInput.text);
        int month = int.Parse(monthInput.text);
        int year = int.Parse(yearInput.text);
        Debug.Log($"Testing for day: {day}/{month}/{year}");
        if (!debug)
        {
            moveRing = _getCurrentMoveRingValue();
            exerciseRing = _getCurrentExerciseRingValue();
            moveGoal = _getMoveRingGoal();
            targetMove = _getMoveRingValue(day, month, year);
        }
        
        
        

        healthText.text = $"MoveRing: {moveRing}\nExerciseRing: {exerciseRing}\nGoal: {moveGoal}\n\nOther Date: ";
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
