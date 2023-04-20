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
    private static extern double _getCurrentMoveRingGoal();

    [DllImport("__Internal")]
    private static extern double _getMoveRingValue(int day, int month, int year);

    [DllImport("__Internal")]
    private static extern double _getMoveRingGoal(int day, int month, int year);

    [DllImport("__Internal")]
    private static extern double _getExerciseRingValue(int day, int month, int year);

    public bool debug;
    public double moveRing;
    public double exerciseRing;
    public double moveGoal;

    public double targetMove;
    public double targetExercise;
    public double targetMoveGoal;

    public double altTargetMove;
    public double altTargetExercise;
    public double altTargetMoveGoal;

    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        if (!debug) 
        {
            _requestAuthorization();
            moveRing = _getCurrentMoveRingValue();
            exerciseRing = _getCurrentExerciseRingValue();
            moveGoal = _getCurrentMoveRingGoal();
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
            moveGoal = _getCurrentMoveRingGoal();
            targetMove = _getMoveRingValue(day, month, year);
            targetExercise = _getExerciseRingValue(day, month, year);
            targetMoveGoal = _getMoveRingGoal(day, month, year);
            altTargetMove = _getMoveRingValue(16, 4, 2023);
            altTargetExercise = _getExerciseRingValue(16, 4, 2023);
            altTargetMoveGoal = _getMoveRingGoal(16, 4, 2023);
        }

        healthText.text = $"MoveRing: {moveRing}\nExerciseRing: {exerciseRing}\nGoal: {moveGoal}\n\nOther Date:\n {targetMove}\n{targetExercise}\n{targetMoveGoal}\n\nAlt Other Date:\n {altTargetMove}\n{altTargetExercise}\n{altTargetMoveGoal}";
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
