using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class HealthHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void _requestAuthorization();

    [DllImport("__Internal")]
    private static extern double _getCurrentMoveRingValue();

    [DllImport("__Internal")]
    private static extern double _getCurrentExerciseRingValue();

    [DllImport("__Internal")]
    private static extern double _getMoveRingGoal();

    public double moveRing;
    public double exerciseRing;
    public double moveGoal;

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        _requestAuthorization();
        moveRing = _getCurrentMoveRingValue();
        exerciseRing = _getCurrentExerciseRingValue();
        moveGoal = _getMoveRingGoal();
    }

    // Update is called once per frame
    void Update()
    {
        moveRing = _getCurrentMoveRingValue();
        exerciseRing = _getCurrentExerciseRingValue();
        moveGoal = _getMoveRingGoal();
        healthText.text = $"MoveRing: {moveRing}\nExerciseRing: {exerciseRing}\nGoal: {moveGoal}";
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
